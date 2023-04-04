using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NorthwindEmployeeAPI.Controllers;
using NorthwindEmployeeAPI.Data.Repositories;
using NorthwindEmployeeAPI.Models;
using NorthwindEmployeeAPI.Services;

namespace NorthwindEmployeeAPITests
{
    public class EmpolyeeControllerUpdateTests
    {
        [Category("Happy Path")]
        [Category("PutEmployeeDetails")]
        [Test]
        public async Task GivenValidID_PutEmployeeDetails_ReturnsNoContext()
        {
            var mockEmployeeService = new Mock<INorthwindService<Employee>>();
            var mockTerritoryService = new Mock<INorthwindService<Territory>>();
            var mockOrderService = new Mock<IOrderService<Order>>();
            int id = 1;
            var employee = new Employee
            {
                EmployeeId = 1,
                FirstName = "Charlie",
                LastName = "Evans",
                Title = "Sales Representative",
                Address = "67 Minehead Street",
                City = "Leicester",
                PostalCode = "LE3 0SJ",
                Country = "UK"
            };

            mockEmployeeService
                .Setup(cs => cs.UpdateAsync(1, employee))
                .ReturnsAsync(true);

            var sut = new EmployeesController(mockEmployeeService.Object,mockOrderService.Object, mockTerritoryService.Object);
            var result = await sut.PutEmployeeDetails(id, employee);

            mockEmployeeService.Verify(cs => cs.UpdateAsync(id, employee), Times.Once);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Category("Happy Path")]
        [Category("PutEmployeeTerritory")]
        [Test]
        public async Task GivenValidID_PutEmployeeTerritoryIdList_ReturnsNoContext()
        {
            var mockEmployeeService = new Mock<INorthwindService<Employee>>();
            var mockTerritoryService = new Mock<INorthwindService<Territory>>();
            var mockOrderService = new Mock<IOrderService<Order>>();
            int id = 1;

            List<string> territoryIds = Mock.Of<List<string>>();

            var mockEmployee = new Employee();

            mockEmployeeService
                .Setup(es => es.GetAsync(id))
                .ReturnsAsync(mockEmployee);

            mockEmployeeService
                .Setup(es => es.UpdateAsync(1, mockEmployee))
                .ReturnsAsync(true);

            var sut = new EmployeesController(mockEmployeeService.Object, mockOrderService.Object, mockTerritoryService.Object);
            var result = await sut.PutEmployeeTerritory(id, territoryIds);

            mockEmployeeService.Verify(cs => cs.UpdateAsync(id, mockEmployee), Times.Once);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Category("Sad Path")]
        [Category("PutEmployeeTerritory")]
        [Test]
        public async Task GivenNullId_PutEmployeeTerritory_ReturnsError()
        {
            var mockEmployeeService = new Mock<INorthwindService<Employee>>();
            var mockTerritoryService = new Mock<INorthwindService<Territory>>();
            var mockOrderService = new Mock<IOrderService<Order>>();
            int id = 1;

            List<string> territoryIds = new List<string> { null };

            var mockEmployee = new Employee();

            mockEmployeeService
                .Setup(es => es.GetAsync(id))
                .ReturnsAsync(mockEmployee);

            var sut = new EmployeesController(mockEmployeeService.Object, mockOrderService.Object, mockTerritoryService.Object);
            var result = await sut.PutEmployeeTerritory(id, territoryIds);

            //Assert.That(result, Is.False);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
}