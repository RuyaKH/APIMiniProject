using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindEmployeeAPI.Data.Repositories;
using NorthwindEmployeeAPI.Models;
using NorthwindEmployeeAPI.Models.DTO;
using NorthwindEmployeeAPI.Services;

//
namespace NorthwindEmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService<Employee> _employeeService;
        private readonly IOrderService<Order> _orderService;

        public EmployeesController(IEmployeeService<Employee> employeeService, IOrderService<Order> orderService)
        {
            _employeeService = employeeService;
            _orderService = orderService;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployeesAsync()
        {
          if (await _employeeService.GetAllAsync() != null)
          {
                return (await _employeeService.GetAllAsync())!
                    .Select(e => Utils.ToEmployeeDTO(e))
                    .ToList();
          }
            return NotFound();
        }


        [HttpGet("MostItems")]
        public async Task<ActionResult<string>> GetsHighestNumberOfProductsAsync()
        {
            var result = _orderService.HighestQuantityOfOrderAsync().Result;
            if (result == null) return NotFound();
            return result;
        }

        [HttpGet("MonthlySales")]
        public async Task<ActionResult<List<object>>> GetMonthlySalesAsync()
        {
            var result = _orderService.SalesByMonthAsync().Result;
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
            var result =await _employeeService.EmployeeReportToAsync();
            if (result.Count == 0) return NotFound();
            return result;
        }
        // GET: api/Employees/5
        [HttpGet("{id}")]
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

            return Utils.ToEmployeeDTO(employee);
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/territory")]
        public async Task<IActionResult> PutEmployeeTerritoryRegion(int id,
            [Bind("EmployeeId", "TerritoryId", "TerritoryDescription")] Employee employee)
        {
            
            if (id != employee.EmployeeId)
            {
                return BadRequest();
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
