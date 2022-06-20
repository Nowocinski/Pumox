using PumoxWebApplication.Models.Enumes;

namespace PumoxWebApplication.DTOs
{
    public class SearchRequestDTO
    {
        public string Keyword { get; set; }
        public DateTime? EmployeeDateOfBirthFrom { get; set; }
        public DateTime? EmployeeDateOfBirthTo { get; set; }
        public IEnumerable<JobTitle> EmployeeJobTitles { get; set; }
    }
}
