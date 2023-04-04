﻿using System;
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

        private readonly NorthwindContext _context;
        private readonly INorthwindRepository<Employee> _employeeRepository;
        private readonly INorthwindService<Employee> _employeeService;
        private readonly IOrderService<Order> _orderService;
        private readonly INorthwindService<Territory> _territoryService;
        public EmployeesController(NorthwindContext context, INorthwindRepository<Employee> employeeRepository,
            INorthwindService<Employee> employeeService, IOrderService<Order> orderService, INorthwindService<Territory> territoryService)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
            _context = context;
            _orderService = orderService;
            _territoryService = territoryService;
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
        [HttpPut("{id}/territory")]
        public async Task<IActionResult> PutEmployeeTerritoryRegion(int id, List<string> territoryIds)
            //[Bind("EmployeeId", "TerritoryId", "TerritoryDescription")] Employee employee)
        {
            var employee = _employeeService.GetAsync(id).Result;
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }
            employee.Territories.Clear();
            foreach(string territoryId in territoryIds)
            {
                employee.Territories.Add(_territoryService.GetAsync(territoryId).Result);
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
