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
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ILogger<WeatherForecastController> logger, ICompanyRepository companyRepository)
        {
            _logger = logger;
            _companyRepository = companyRepository;
        }

        [HttpPost("create")]
        public async Task<object> CreateCompany(CreateCompanyDTO createCompanyDTO)
        {
            Company company = new Company
            {
                Name = createCompanyDTO.Name,
                EstablishmentYear = createCompanyDTO.EstablishmentYear,
                Employees = createCompanyDTO.Employees.Select(employee => new Employee
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    DateOfBirth = employee.DateOfBirth,
                    // TODO: Do poprawienia
                    JobTitle = Models.Enumes.JobTitle.Other
                }).ToList()
            };
            await _companyRepository.CreateAsync(company);
            await _companyRepository.SaveChangesAsync();
            return new
            {
                Id = company.Id
            };
        }
    }
}
