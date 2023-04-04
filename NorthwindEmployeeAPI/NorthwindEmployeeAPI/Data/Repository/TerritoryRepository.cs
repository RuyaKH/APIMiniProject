using Microsoft.EntityFrameworkCore;
using NorthwindEmployeeAPI.Data.Repositories;
using NorthwindEmployeeAPI.Models;

namespace NorthwindEmployeeAPI.Data.Repository
{
    public class TerritoryRepository : NorthwindRepository<Territory>
    {
        public TerritoryRepository(NorthwindContext context) : base(context)
        {

        }

        public override async Task<Territory?> FindAsync(string id)
        {
            return await _dbSet
                .Where(t => t.TerritoryId == id)
                .Include(t => t.Employees)
                .FirstOrDefaultAsync();
        }
    }
}
