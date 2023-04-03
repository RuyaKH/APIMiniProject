using Microsoft.EntityFrameworkCore;
using NorthwindEmployeeAPI.Data.Repositories;
using NorthwindEmployeeAPI.Models;

namespace NorthwindEmployeeAPI.Data.Repository
{
    public class OrderRepository : NorthwindRepository<Order>
    {
        public OrderRepository(NorthwindContext context) : base(context)
        {

        }
        public override async Task<Order?> FindAsync(int id)
        {
            return await _dbSet
                .Where(e => e.OrderId == id)
                .Include(e => e.OrderDetails)
                .FirstOrDefaultAsync();
        }
        public override async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _dbSet
                .Include(e => e.OrderDetails)
                .ToListAsync();
        }
    }
}
