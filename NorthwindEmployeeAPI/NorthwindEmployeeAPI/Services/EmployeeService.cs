using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Services;
using NorthwindEmployeeAPI.Data.Repositories;
using NorthwindEmployeeAPI.Data.Repository;
using NorthwindEmployeeAPI.Models;
using NorthwindEmployeeAPI.Models.DTO;

namespace NorthwindEmployeeAPI.Services
{
    public class EmployeeService : NorthwindServices<Employee>, IEmployeeService<Employee>
    {
        private readonly INorthwindRepository<Employee> _employeeRepository;
        public EmployeeService(ILogger<INorthwindService<Employee>> logger, INorthwindRepository<Employee> repository) : base(logger, repository)
        {
            _employeeRepository = repository;
        }
        public async Task<List<object>> EmployeeReportToAsync()
        {
            var result = _employeeRepository.returnContext().Employees
            .Join(_employeeRepository.returnContext().Employees, e1 => e1.EmployeeId, e2 => e2.ReportsTo, (e1, e2) => new
            {
                employee = e2.FirstName + " " + e2.LastName,
                reportsto = e1.FirstName + " " + e1.LastName,
            });
            return result.Cast<object>().ToList();
        }

        public override async Task<bool> UpdateAsync(int id, Employee entity)
        {
            var original = _employeeRepository.FindAsync(id).Result;

            if (original == null)
                return false;

            if (entity.Address != null)
                original.Address = entity.Address;
            if (entity.BirthDate != null)
                original.BirthDate = entity.BirthDate;
            if (entity.City != null)
                original.City = entity.City;
            if (entity.Country != null)
                original.Country = entity.Country;
            if (entity.FirstName != null)
                original.FirstName = entity.FirstName;
            if (entity.HireDate != null)
                original.HireDate = entity.HireDate;
            if (entity.HomePhone != null)
                original.HomePhone = entity.HomePhone;
            if (entity.LastName != null)
                original.LastName = entity.LastName;
            if (entity.Notes != null)
                original.Notes = entity.Notes;
            if (entity.PostalCode != null)
                original.PostalCode = entity.PostalCode;
            if (entity.Title != null)
                original.Title = entity.Title;
            if (entity.TitleOfCourtesy != null)
                original.TitleOfCourtesy = entity.TitleOfCourtesy;
            if (entity.ReportsTo != null)
                original.ReportsTo = entity.ReportsTo;
            if (entity.Region != null)
                original.Region = entity.Region;

            await _employeeRepository.SaveAsync();
            return true;
        }
    }
}
