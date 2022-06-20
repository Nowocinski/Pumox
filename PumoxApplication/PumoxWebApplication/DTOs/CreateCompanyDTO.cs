namespace PumoxWebApplication.DTOs
{
    public class CreateCompanyDTO
    {
        public string Name { get; set; }
        public int EstablishmentYear { get; set; }
        public List<CreateEmployeeDTO> Employees { get; set; }
    }
}
