using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindEmployeeAPI.Models;
using NorthwindEmployeeAPI.Services;

//
namespace NorthwindEmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class EmployeesController : ControllerBase
    {
        private readonly INorthwindService<Employee> _employeeService;
        
        public EmployeesController(NorthwindContext context, INorthwindService<Employee> employeeService, INorthwindService<Territory> territoryService)
        {
            _context = context;
            _employeeService = employeeService;
            _territoryService = territoryService;
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
