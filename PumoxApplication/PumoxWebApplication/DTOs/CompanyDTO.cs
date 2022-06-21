namespace PumoxWebApplication.DTOs
{
    public class CompanyDTO
    {
        public string Name { get; set; }
        public int EstablishmentYear { get; set; }
        public List<EmployeeDTO> Employees { get; set; }
    }
}
