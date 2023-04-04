using AutoMapper;
using NorthwindEmployeeAPI.HATEOAS;
using NorthwindEmployeeAPI.Models;

namespace NorthwindEmployeeAPI;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<Employee, EmployeeModel>();
    }
}
