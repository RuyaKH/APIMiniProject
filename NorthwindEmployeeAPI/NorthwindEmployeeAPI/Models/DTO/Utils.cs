using Microsoft.IdentityModel.Tokens;

namespace NorthwindEmployeeAPI.Models.DTO
{
    public class Utils
    {
        public static EmployeeDTO ToEmployeeDTO(Employee employee) => new EmployeeDTO
        {
            EmployeeId = employee.EmployeeId,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Title = employee.Title,
            NumberOfOrders = employee.Orders.Count(),
            TotalMoneyMade = employee.Orders
                .Sum(x => x.OrderDetails
                .Sum(od => od.UnitPrice * od.Quantity)),
            AverageSale = employee.Orders.IsNullOrEmpty() ? 0 : Math.Round(employee.Orders
                .Average(x => x.OrderDetails
                .Sum(od => od.UnitPrice * od.Quantity)), 2),
        };
    }
}
