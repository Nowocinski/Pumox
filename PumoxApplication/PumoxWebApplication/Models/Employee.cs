using PumoxWebApplication.Models.Enumes;

namespace PumoxWebApplication.Models
{
    public class Employee
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public JobTitle JobTitle { get; set; }
        public Company Company { get; set; }
        public long Company_Id { get; set; }
    }
}
