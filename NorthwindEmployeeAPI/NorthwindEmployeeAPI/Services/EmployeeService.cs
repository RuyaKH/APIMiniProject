using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Services;
using NorthwindEmployeeAPI.Data.Repositories;
using NorthwindEmployeeAPI.Models;
using NorthwindEmployeeAPI.Models.DTO;

namespace NorthwindEmployeeAPI.Services
{
    public class EmployeeService : NorthwindServices<Employee>
    {
        private readonly NorthwindRepository<Order> _repository;
        public EmployeeService(ILogger<INorthwindService<Employee>> logger, INorthwindRepository<Employee> repository) : base(logger, repository)
        {
        }
        public async Task<Employee> HighestQuantityOfOrder(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
