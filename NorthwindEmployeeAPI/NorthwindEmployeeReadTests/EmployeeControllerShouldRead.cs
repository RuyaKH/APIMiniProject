using Microsoft.Identity.Client;
using Moq;
using NorthwindEmployeeAPI.Controllers;
using NorthwindEmployeeAPI.Data.Repository;
using NorthwindEmployeeAPI.Models;
using NorthwindEmployeeAPI.Models.DTO;
using NorthwindEmployeeAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindEmployeeReadTests;

    public class EmployeeControllerTest
    {
        [Test]
        public async Task GetEmployees()
        {
            var mockService = new Mock<INorthwindService<Employee>>();
            var mockService2 = new Mock<INorthwindService<Order>>();
            var mockService3 = new Mock<INorthwindService<Territory>>();
            //new Employee
            //{
            //    EmployeeId = 1,
            //    FirstName = "Alex",
            //    LastName = "ALEX",
            //    Title = "Master",
            //};
            List<Employee> employees = new List<Employee> {new Employee{
            EmployeeId = 1,
            FirstName = "Alex",
            LastName = "ALEX",
            Title = "Master",
        }};
            mockService
                .Setup(sc => sc.GetAllAsync().Result)
                .Returns(employees);
            var sut = new EmployeesController(mockService.Object, mockService2.Object, mockService3.Object);
            var result = await sut.GetEmployees();
            Assert.That(result.Value, Is.InstanceOf<List<EmployeeDTO>>());
        }
    }
}
