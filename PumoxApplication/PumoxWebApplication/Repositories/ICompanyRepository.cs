using PumoxWebApplication.Models;
using System.Linq.Expressions;

namespace PumoxWebApplication.Repositories
{
    public interface ICompanyRepository
    {
        Task CreateAsync(Company company);
        IQueryable<Company> GetAsync(Expression<Func<Company, bool>> filter = null);
        IQueryable<Company> GetAsync();
        Task<Company> GetSingleAsync(Expression<Func<Company, bool>> filter = null);
        void Update(Company company);
        void Delete(Company company);
        Task SaveChangesAsync();
    }
}
