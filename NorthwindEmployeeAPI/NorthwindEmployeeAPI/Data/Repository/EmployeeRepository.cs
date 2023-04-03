using Microsoft.EntityFrameworkCore;
using NorthwindEmployeeAPI.Data.Repositories;
using NorthwindEmployeeAPI.Models;

namespace NorthwindEmployeeAPI.Data.Repository
{
    public class EmployeeRepository : NorthwindRepository<Employee>
    {
        public EmployeeRepository(NorthwindContext context) : base(context)
        {

        }

        public override async Task<Employee?> FindAsync(int id)
        {
            return await _dbSet
                .Where(e => e.EmployeeId == id)
                .Include(e => e.Territories)
                .Include(e => e.Orders)
                .FirstOrDefaultAsync();
        }
        public override async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _dbSet
                .Include(e => e.Territories)
                .Include(e => e.Orders)
                .ToListAsync();
        }
    }
}
