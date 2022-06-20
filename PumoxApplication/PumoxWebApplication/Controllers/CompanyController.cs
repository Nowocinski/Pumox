using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PumoxWebApplication.DTOs;
using PumoxWebApplication.Models;
using PumoxWebApplication.Repositories;

namespace PumoxWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ILogger<CompanyController> logger, ICompanyRepository companyRepository)
        {
            _logger = logger;
            _companyRepository = companyRepository;
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<object> CreateCompany(CompanyDTO createCompanyDTO)
        {
            // TODO: Dodać walidacje
            Company company = new Company
            {
                Name = createCompanyDTO.Name,
                EstablishmentYear = createCompanyDTO.EstablishmentYear,
                Employees = createCompanyDTO?.Employees.Select(employee => new Employee
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    DateOfBirth = employee.DateOfBirth,
                    // TODO: Do poprawienia - w pdf jest podany string
                    JobTitle = employee.JobTitle
                }).ToList()
            };
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
            // TODO: Dodać walidację
            // TODO: Spróbować uprościć filtrowanie
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

            if (searchRequestDTO.EmployeeDateOfBirthFrom != null)
            {
                companies.ToList()
                    .ForEach(company => company.Employees.ToList().ForEach(employee =>
                    {
                        if (employee.DateOfBirth < searchRequestDTO.EmployeeDateOfBirthFrom)
                        {
                            company.Employees.Remove(employee);
                        }
                    }));
            }

            if (searchRequestDTO.EmployeeDateOfBirthTo != null)
            {
                companies.ToList()
                    .ForEach(company => company.Employees.ToList().ForEach(employee =>
                    {
                        if (employee.DateOfBirth > searchRequestDTO.EmployeeDateOfBirthTo)
                        {
                            company.Employees.Remove(employee);
                        }
                    }));
            }

            if (searchRequestDTO.EmployeeJobTitles != null)
            {
                companies.ToList()
                    .ForEach(company => company.Employees.ToList().ForEach(employee =>
                    {
                        if (!searchRequestDTO.EmployeeJobTitles.Any(jobTitle => jobTitle == employee.JobTitle))
                        {
                            company.Employees.Remove(employee);
                        }
                    }));
            }

            return new
            {
                Results = companies.Select(company => new SearchResponseDTO
                {
                    Name = company.Name,
                    EstablishmentYear = company.EstablishmentYear,
                    Employees = company.Employees.Select(employee => new EmployeesDTO
                    {
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        DateOfBirth = employee.DateOfBirth,
                        JobTitle = employee.JobTitle
                    })
                })
            };
        }

        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCompany(long id, CompanyDTO companyDTO)
        {
            // TODO: Dodać walidację
            var company = await _companyRepository.GetSingleAsync(company => company.Id == id);
            if (company == null)
            {
                return NotFound($"No company found with id: {id}");
            }

            company.Name = companyDTO.Name;
            company.EstablishmentYear = companyDTO.EstablishmentYear;
            company.Employees = companyDTO.Employees.Select(employee => new Employee
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                JobTitle = employee.JobTitle
            }).ToList();

            _companyRepository.Update(company);
            await _companyRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCompany(long id)
        {
            // TODO: Dodać walidację
            var company = await _companyRepository.GetSingleAsync(company => company.Id == id);
            if (company == null)
            {
                return NotFound($"No company found with id: {id}");
            }

            _companyRepository.Delete(company);
            await _companyRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}
