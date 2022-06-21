using PumoxWebApplication.Models.Enumes;

namespace PumoxWebApplication.DTOs
{
    public class EmployeeDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public JobTitle JobTitle { get; set; }
    }
}
