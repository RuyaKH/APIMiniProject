namespace NorthwindEmployeeAPI.Models.DTO
{
    public class Utils
    {
        public static EmployeeDTO ToEmployeeDTO (Employee employee)
        {
            return new EmployeeDTO
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Title = employee.Title,
                NumberOfOrders = employee.Orders.Count(),
                Metric2 = employee.Orders.Count()
            };
        }
    }
}
