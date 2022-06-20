using PumoxWebApplication.DTOs;
using PumoxWebApplication.Models;

namespace PumoxWebApplication.Repositories
{
    public interface ICompanyRepository
    {
        Task CreateAsync(Company company);
        Task SaveChangesAsync();
    }
}
