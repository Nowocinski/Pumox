using FluentValidation;
using PumoxWebApplication.DTOs;

namespace PumoxWebApplication.Validators
{
    public class CompanyDTOValidator : AbstractValidator<CompanyDTO>
    {
        public CompanyDTOValidator(IValidator<EmployeeDTO> employeeDTOvalidator)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Company name cannot be empty");
            RuleForEach(x => x.Employees).SetValidator(employeeDTOvalidator);
        }
    }
}
