namespace PumoxWebApplication.DTOs
{
    public class SearchResponseDTO
    {
        public string Name { get; set; }
        public int EstablishmentYear { get; set; }
        public IEnumerable<EmployeeDTO> Employees { get; set; }
    }
}
