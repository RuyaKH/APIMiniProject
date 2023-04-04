using Microsoft.AspNetCore.Http.HttpResults;
using NorthwindAPI.Services;
using NorthwindEmployeeAPI.Data.Repositories;
using NorthwindEmployeeAPI.Models;

namespace NorthwindEmployeeAPI.Services;

public class EmployeeServices : NorthwindService<Employee>
{
    private INorthwindRepository<Employee> _employeeRepository;
    private ILogger _logger;

    public EmployeeServices(ILogger<INorthwindService<Employee>> logger, INorthwindRepository<Employee> repository) : base(logger, repository)
    {
        _employeeRepository = repository;
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
