using Microsoft.AspNetCore.Mvc;
using Moq;
using NorthwindEmployeeAPI.Controllers;
using NorthwindEmployeeAPI.Models;
using NorthwindEmployeeAPI.Services;

namespace NorthwindEmployeeAPITests
{
    internal class CreateEmployeeTests
    {
        [Test]
        public async Task GivenAValidEmployee_PostEmployee_InsertsEmployee()
        {
            var mockService = Mock.Of<INorthwindService<Employee>>();
            var mockOrders = Mock.Of<INorthwindService<Order>>();
            var mockTerritory = Mock.Of<INorthwindService<Territory>>();
            Mock.Get(mockService)
                .Setup(es => es.CreateAsync(It.IsAny<Employee>()).Result)
                .Returns(true);
            var _sut = new EmployeesController(mockService, mockOrders, mockTerritory);
            var result = await _sut.PostEmployee(new Employee());
            Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        }
        [Test]
        public async Task GivenInvalidEmlpoyee_PostEmployee_ReturnsProblem()
        {
            var mockService = Mock.Of<INorthwindService<Employee>>();
            var mockOrders = Mock.Of<INorthwindService<Order>>();
            var mockTerritory = Mock.Of<INorthwindService<Territory>>();
            Mock.Get(mockService)
                .Setup(es => es.CreateAsync(It.IsAny<Employee>()).Result)
                .Returns(false);
            var _sut = new EmployeesController(mockService, mockOrders, mockTerritory);
            var result = await _sut.PostEmployee(Mock.Of<Employee>());
            Assert.That(result, Is.TypeOf<ActionResult<Employee>>());
            Assert.That(result.Result, Is.TypeOf<BadRequestResult>());
        }
    }
}
