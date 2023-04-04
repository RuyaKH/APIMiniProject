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

        // DELETE: api/Employees/5
        /*[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
