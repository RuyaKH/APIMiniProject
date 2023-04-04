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

namespace NorthwindEmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly NorthwindContext _context;
        private readonly INorthwindRepository<Employee> _employeeRepository;
        private readonly INorthwindService<Employee> _employeeService;
        private readonly IOrderService<Order> _orderService;
        public EmployeesController(NorthwindContext context, INorthwindRepository<Employee> employeeRepository,
            INorthwindService<Employee> employeeService, IOrderService<Order> orderService)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
            _context = context;
            _orderService = orderService;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
          if (await _employeeService.GetAllAsync() != null)
          {
                return (await _employeeService.GetAllAsync())!
                    .Select(e => Utils.ToEmployeeDTO(e))
                    .OrderBy(e => e.Metric2)
                    .ToList();
          }
            return NotFound();
        }


        [HttpGet("testing")]
        public async Task<ActionResult<string>> Testing()
        {
            var result = _orderService.HighestQuantityOfOrderAsync().Result;
            return result;
        }

        [HttpGet("testingtwo")]
        public async Task<ActionResult<List<object>>> TestingTwo()
        {
            var result = _orderService.SalesByMonthAsync().Result;

            
            return result;
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
          if (_context.Employees == null)
          {
              return Problem("Entity set 'NorthwindContext.Employees'  is null.");
          }
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
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
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
