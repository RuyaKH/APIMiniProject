using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NorthwindEmployeeAPI.HATEOAS;
using NorthwindEmployeeAPI.Models;
using NorthwindEmployeeAPI.Models.DTO;
using NorthwindEmployeeAPI.Services;


namespace NorthwindEmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class EmployeesController : ControllerBase
    {
        //private readonly IReadOnlyList<ActionDescriptor> _routes;
        //private readonly IMapper _mapper;
        private readonly INorthwindService<Employee> _employeeService;
        private readonly INorthwindService<Order> _orderService;
        private readonly INorthwindService<Territory> _territoryService;
        
        public EmployeesController(
            INorthwindService<Employee> employeeService,
            INorthwindService<Order> orderService,
            INorthwindService<Territory> territoryService) 
        {
            _employeeService = employeeService;
            _orderService = orderService;
            _territoryService = territoryService;
        }

        // GET: api/Employees
        [HttpGet(Name = nameof(GetEmployeesAsync))]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployeesAsync()
        {
            var employee = await _employeeService.GetAllAsync();

            if (employee == null)
            {
                return NotFound();
            }

            var employeeModel = employee
                .Select(e => CreateLinksForUser(Utils.ToEmployeeDTO(e)))
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
                    .Select(e => CreateLinksForUser(Utils.ToEmployeeDTO(e)))
                    .OrderByDescending(e => e.TotalMoneyMade)
                    .First();
            }
            return NotFound();
        }

        [HttpGet("ReportsTo")]
        public async Task<ActionResult<List<object>>> GetsReportsToAsync()
        {
            var result = await _employeeService.GetColumnToAsync();
            if (result.Count == 0) return NotFound();
            return result;
        }

        // GET: api/Employees/5
        [HttpGet("{id}",Name = nameof(GetEmployeeAsync))]
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

            return Ok(CreateLinksForUser(Utils.ToEmployeeDTO(employee)));
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            bool inserted = await _employeeService.CreateAsync(employee);
            if (!inserted) return BadRequest();
            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
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


        // DELETE: api/Employees/5
        [HttpDelete("{id}", Name = nameof(DeleteEmployee))]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = _employeeService.GetAsync(id).Result;
            if (employee == null)
            {
                return NotFound();
            }

            foreach (var territory in employee.Territories)
            {
                territory.Employees.Remove(employee);
            }

            foreach (var order in employee.Orders)
            {
                order.EmployeeId = null;
            }

            var deleted = await _employeeService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Employees/5/Territories/10153
        [HttpDelete("{employeeId}/Territories/{territoryId}")]
        public async Task<IActionResult> DeleteEmployeeTerritory(int employeeId, string territoryId)
        {
            var employee = _employeeService.GetAsync(employeeId).Result;
            if (employee == null)
            {
                return NotFound();
            }
            var territory = _territoryService.GetAsync(territoryId).Result;
            if (territory == null)
            {
                return NotFound();
            }

            bool removed = employee.Territories.Remove(territory);
            if (!removed)
            {
                return NotFound();
            }

            removed = territory.Employees.Remove(employee);
            if (!removed)
            {
                employee.Territories.Add(territory);
                return NotFound();
            }
            await _employeeService.SaveAsync();

            return NoContent();
        }

        private EmployeeDTO CreateLinksForUser(EmployeeDTO employeeDTO)
        {
            var idObj = new { id = employeeDTO.EmployeeId };
            employeeDTO.linkDto.Add(
            new LinkDTO(Url.Link(nameof(this.GetEmployeeAsync), idObj),
            "self",
            "GET"));

            employeeDTO.linkDto.Add(
                new LinkDTO(Url.Link(nameof(this.GetEmployeesAsync), idObj),
                "whole_list_employee",
                "GET"));

            employeeDTO.linkDto.Add(
            new LinkDTO(Url.Link(nameof(this.DeleteEmployee), idObj),
            "delete_employee",
            "DELETE"));

            return employeeDTO;
        }
    }
}
