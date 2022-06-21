using FluentValidation;
using PumoxWebApplication.DTOs;

namespace PumoxWebApplication.Validators
{
    public class EmployeeDTOValidator : AbstractValidator<EmployeeDTO>
    {
        public EmployeeDTOValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("The employee's first name cannot be empty");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("The employee's last name cannot be empty");
            RuleFor(x => x.JobTitle).IsInEnum().WithMessage("There is no definition of this job title");
        }
    }
}
