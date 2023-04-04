using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Services;
using NorthwindEmployeeAPI.Data.Repositories;
using NorthwindEmployeeAPI.Models;


namespace NorthwindEmployeeAPI.Services
{
    public class OrderService : NorthwindServices<Order>, IOrderService<Order>
    {
        private INorthwindRepository<Order> _repository;

        public OrderService(ILogger<INorthwindService<Order>> logger, INorthwindRepository<Order> repository) : base(logger, repository)
        {
            _repository = repository;
        }
        public async Task<string?> HighestQuantityOfOrderAsync()
        {
            var result = await _repository.returnContext().OrderDetails
                 .GroupBy(od => od.Order.EmployeeId)
                 .Select(g => new
                 {
                     EmployeeId = g.Key,
                     TotalQuantity = g.Sum(od => od.Quantity)
                 })
                 .OrderByDescending(g => g.TotalQuantity)
                 .FirstOrDefaultAsync();

            return result?.ToString();
        }

        public async Task<List<object>> SalesByMonthAsync()
        {
            var result = _repository.returnContext().OrderDetails
                .GroupBy(od => new { Year = od.Order.OrderDate.Value.Year, Month = od.Order.OrderDate.Value.Month })
                .Select(g => new { Month = g.Key.Month, Year = g.Key.Year, TotalSales = g.Sum(od => od.Quantity * od.UnitPrice) })
                .OrderBy(r => r.Year)
                .ThenBy(r => r.Month)
                .ToListAsync().Result;

            return result.Cast<object>().ToList();
        }


    }
}
