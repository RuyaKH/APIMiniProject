using Microsoft.AspNetCore.Mvc;
using NorthwindEmployeeAPI.Models;
using NorthwindEmployeeAPI.Services;

namespace NorthwindEmployeeAPI.Controllers;

public partial class EmployeesController
{
    private readonly INorthwindService<Territory> _territoryService;

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

        return NoContent();
    }

    // DELETE: api/Employees/5
    [HttpDelete("{id}")]
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
}
