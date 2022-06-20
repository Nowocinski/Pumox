using PumoxWebApplication.Context;
using PumoxWebApplication.Models;

namespace PumoxWebApplication.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        DataBaseContext _context;
        public CompanyRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Company company)
        {
            await _context.AddAsync(company);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
