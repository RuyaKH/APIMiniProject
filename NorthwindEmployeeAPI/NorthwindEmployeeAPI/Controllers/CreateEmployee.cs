using Microsoft.AspNetCore.Mvc;
using NorthwindEmployeeAPI.Models;

namespace NorthwindEmployeeAPI.Controllers
{
    public partial class EmployeesController
    {
        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            bool inserted = await _employeeService.CreateAsync(employee);
            if (!inserted) return BadRequest();
            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
        }
    }
}
