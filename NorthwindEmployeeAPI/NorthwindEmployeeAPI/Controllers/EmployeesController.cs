using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NorthwindEmployeeAPI.Models;
using NorthwindEmployeeAPI.Models.DTO;
using NorthwindEmployeeAPI.Services;


namespace NorthwindEmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class EmployeesController : RestControllerBase
    {
        //private readonly IReadOnlyList<ActionDescriptor> _routes;
        //private readonly IMapper _mapper;
        private readonly INorthwindService<Employee> _employeeService;
        private readonly INorthwindService<Order> _orderService;
        private readonly INorthwindService<Territory> _territoryService;
        
        public EmployeesController(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider,
            INorthwindService<Employee> employeeService,
            INorthwindService<Order> orderService,
            INorthwindService<Territory> territoryService,
            IMapper mapper) 
            : base(actionDescriptorCollectionProvider, mapper)
        {
            _employeeService = employeeService;
            _orderService = orderService;
            _territoryService = territoryService;
        }

        // GET: api/Employees
        [HttpGet(Name = "GetEmployees")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployeesAsync()
        {
            var employee = await _employeeService.GetAllAsync();

            if (employee == null)
            {
                return NotFound();
            }

            var employeeModel = employee
                .Select(e => Utils.ToEmployeeDTO(e))
                .ToList();

            return Ok(employeeModel);
        }


        [HttpGet("MostItems")]
        public async Task<ActionResult<string>> GetsHighestNumberOfProductsAsync()
        {
            var result = _orderService.HighestQuantityOfColumnAsync().Result;
            if (result == null) return NotFound();
            return result;
        }

        [HttpGet("MonthlySales")]
        public async Task<ActionResult<List<object>>> GetMonthlySalesAsync()
        {
            var result = _orderService.GetMetricAsync().Result;
            if (result.Count == 0) return NotFound();
            return result;
        }
        [HttpGet("MostProfitable")]
        public async Task<ActionResult<EmployeeDTO>> GetsMostMoneyMakingEmployeeAsync()
        {
            if (await _employeeService.GetAllAsync() != null)
            {
                return (await _employeeService.GetAllAsync())!
                    .Select(e => Utils.ToEmployeeDTO(e))
                    .OrderByDescending(e => e.TotalMoneyMade)
                    .First();
            }
            return NotFound();
        }
        [HttpGet("ReportsTo")]
        public async Task<ActionResult<List<object>>> GetsReportsToAsync()
        {
            var result =await _employeeService.GetColumnToAsync();
            if (result.Count == 0) return NotFound();
            return result;
        }

        // GET: api/Employees/5
        [HttpGet("{id}",Name = "GetAnEmployee")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeAsync(int id)
        {
          if (_employeeService == null)
          {
              return NotFound();
          }
            var employee = await _employeeService.GetAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeModel = Utils.ToEmployeeDTO(employee);
            return Ok(RestfulEmployee(employeeModel));
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/territory")]
        public async Task<IActionResult> PutEmployeeTerritory(int id, List<string> territoryIds)
        //[Bind("EmployeeId", "TerritoryId", "TerritoryDescription")] Employee employee)
        {
            var employee = _employeeService.GetAsync(id).Result;

            employee.Territories.Clear();
            foreach (string territoryId in territoryIds)
            {
                var territory = _territoryService.GetAsync(territoryId).Result;
                if (territory != null)
                    employee.Territories.Add(territory);
                else
                    return NotFound("Territory with id " + territoryId + " does not exsist");
            }

            var updatedSuccess = await _employeeService.UpdateAsync(id, employee);
            if (!updatedSuccess) return NotFound();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeDetails(int id, 
            [Bind("EmployeeId", "FirstName", "LastName", "Title", "Address", "City", "PostalCode", "Country")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            var updatedSuccess = await _employeeService.UpdateAsync(id, employee);
            if (!updatedSuccess) return NotFound();

            return NoContent();
        }
    }
}
