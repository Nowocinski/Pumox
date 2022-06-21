using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PumoxWebApplication.DTOs;
using PumoxWebApplication.Models;
using PumoxWebApplication.Models.Enumes;
using PumoxWebApplication.Repositories;

namespace PumoxWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyRepository _companyRepository;
        private readonly IValidator<CompanyDTO> _companyDTOvalidator;
        private readonly IMapper _mapper;

        public CompanyController(ILogger<CompanyController> logger, ICompanyRepository companyRepository, IValidator<CompanyDTO> companyDTOvalidator, IMapper mapper)
        {
            _logger = logger;
            _companyRepository = companyRepository;
            _companyDTOvalidator = companyDTOvalidator;
            _mapper = mapper;
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<object> CreateCompany(CompanyDTO createCompanyDTO)
        {
            var validationResult = await _companyDTOvalidator.ValidateAsync(createCompanyDTO);
            if (validationResult.Errors.Any())
            {
                return BadRequest(validationResult.Errors.First().ErrorMessage);
            }

            var company = _mapper.Map<Company>(createCompanyDTO);

            await _companyRepository.CreateAsync(company);
            await _companyRepository.SaveChangesAsync();
            return new
            {
                Id = company.Id
            };
        }

        [HttpPost("Search")]
        public async Task<object> SearchCompany(SearchRequestDTO searchRequestDTO)
        {
            IEnumerable<Company> companies;
            if (!string.IsNullOrEmpty(searchRequestDTO.Keyword))
            {
                companies = await _companyRepository
                .GetAsync(company => company.Name.ToLower().Contains(searchRequestDTO.Keyword.ToLower())
                || company.Employees.Any(employee => employee.FirstName.ToLower().Contains(searchRequestDTO.Keyword.ToLower()) || employee.LastName.ToLower().Contains(searchRequestDTO.Keyword.ToLower())));
            }
            else
            {
                companies = await _companyRepository.GetAsync();
            }

            this.FilterEmployeesWithDateOfBirthFrom(ref companies, searchRequestDTO.EmployeeDateOfBirthFrom);
            this.FilterEmployeesWithDateOfBirthTo(ref companies, searchRequestDTO.EmployeeDateOfBirthTo);
            this.FilterEmployeesJobTitles(ref companies, searchRequestDTO.EmployeeJobTitles);

            return new
            {
                Results = _mapper.Map<IEnumerable<Company>, IEnumerable<CompanyDTO>>(companies)
            };
        }

        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCompany(long id, CompanyDTO companyDTO)
        {
            var validationResult = await _companyDTOvalidator.ValidateAsync(companyDTO);
            if (validationResult.Errors.Any())
            {
                return BadRequest(validationResult.Errors.First().ErrorMessage);
            }

            var company = await _companyRepository.GetSingleAsync(company => company.Id == id);
            if (company == null)
            {
                return NotFound($"No company found with id: {id}");
            }

            _mapper.Map<CompanyDTO, Company>(companyDTO, company);

            await _companyRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCompany(long id)
        {
            var company = await _companyRepository.GetSingleAsync(company => company.Id == id);
            if (company == null)
            {
                return NotFound($"No company found with id: {id}");
            }

            _companyRepository.Delete(company);
            await _companyRepository.SaveChangesAsync();
            return NoContent();
        }

        private void FilterEmployeesWithDateOfBirthFrom(ref IEnumerable<Company> companies, DateTime? date)
        {
            if (date != null)
            {
                companies.ToList()
                    .ForEach(company => company.Employees.ToList().ForEach(employee =>
                    {
                        if (employee.DateOfBirth < date)
                        {
                            company.Employees.Remove(employee);
                        }
                    }));
            }
        }

        private void FilterEmployeesWithDateOfBirthTo(ref IEnumerable<Company> companies, DateTime? date)
        {
            if (date != null)
            {
                companies.ToList()
                    .ForEach(company => company.Employees.ToList().ForEach(employee =>
                    {
                        if (employee.DateOfBirth > date)
                        {
                            company.Employees.Remove(employee);
                        }
                    }));
            }
        }

        private void FilterEmployeesJobTitles(ref IEnumerable<Company> companies, IEnumerable<JobTitle> jobTitles)
        {
            if (jobTitles != null && jobTitles.Any())
            {
                companies.ToList()
                    .ForEach(company => company.Employees.ToList().ForEach(employee =>
                    {
                        if (!jobTitles.Any(jobTitle => jobTitle == employee.JobTitle))
                        {
                            company.Employees.Remove(employee);
                        }
                    }));
            }
        }
    }
}
