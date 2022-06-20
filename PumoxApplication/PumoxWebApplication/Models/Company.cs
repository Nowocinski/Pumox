namespace PumoxWebApplication.Models
{
    public class Company
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string EstablishmentYear { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}
