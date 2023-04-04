using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using NorthwindEmployeeAPI.HATEOAS;
using NorthwindEmployeeAPI.Models;
using NorthwindEmployeeAPI.Models.DTO;

namespace NorthwindEmployeeAPI.Controllers
{
    public class RestControllerBase : ControllerBase
    {
        private readonly IReadOnlyList<ActionDescriptor> _routes;
        private readonly IMapper _mapper;

        public RestControllerBase(
            IActionDescriptorCollectionProvider actionDescriptorCollectionProvider,
            IMapper mapper)
        {
            _routes = actionDescriptorCollectionProvider.ActionDescriptors.Items;
            _mapper = mapper;
        }

        internal LinkDTO UrlLink(string relation, string routeName, object values)
        {
            var route = _routes.FirstOrDefault(f =>
                                    f.AttributeRouteInfo.Name.Equals(routeName));
            var method = route.ActionConstraints.
                                    OfType<HttpMethodActionConstraint>()
                                    .First()
                                    .HttpMethods
                                    .First();
            var url = Url.Link(routeName, values).ToLower();
            return new LinkDTO(url, relation, method);
        }

        internal EmployeeModel RestfulEmployee(EmployeeDTO employee)
        {
            EmployeeModel employeeModel = _mapper.Map<EmployeeModel>(employee);

            employeeModel.Links.Add(
                UrlLink("all",
                        "GetEmployees",
                        null));

            employeeModel.Links.Add(
                UrlLink("_self",
                        "GetAnEmployee",
                        new { id = employeeModel.EmployeeId }));

            return employeeModel;
        }

    }
}
