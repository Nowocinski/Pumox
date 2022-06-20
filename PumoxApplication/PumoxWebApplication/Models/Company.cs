namespace PumoxWebApplication.Models
{
    public class Company
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int EstablishmentYear { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
