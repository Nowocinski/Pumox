using PumoxWebApplication.Models;
using System.Linq.Expressions;

namespace PumoxWebApplication.Repositories
{
    public interface ICompanyRepository
    {
        Task CreateAsync(Company company);
        Task<IEnumerable<Company>> GetAsync(Expression<Func<Company, bool>> filter = null);
        Task SaveChangesAsync();
    }
}
