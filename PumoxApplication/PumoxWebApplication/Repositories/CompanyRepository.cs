using Microsoft.EntityFrameworkCore;
using PumoxWebApplication.Context;
using PumoxWebApplication.Models;
using System.Linq.Expressions;

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

        public async Task<IEnumerable<Company>> GetAsync(Expression<Func<Company, bool>> filter)
        {
            return await _context.Companies
                .Where(filter)
                .Include(column => column.Employees)
                .ToListAsync();
        }

        public async Task<IEnumerable<Company>> GetAsync()
        {
            return await _context.Companies
                .Include(column => column.Employees)
                .ToListAsync();
        }

        public async Task<Company> GetSingleAsync(Expression<Func<Company, bool>> filter = null)
        {
            return await _context.Companies
                .Include(column => column.Employees)
                .FirstOrDefaultAsync(filter);
        }

        public void Update(Company company)
        {
            _context.Companies.Update(company);
        }

        public void Delete(Company company)
        {
            _context.Companies.Remove(company);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
