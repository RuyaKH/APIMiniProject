using Microsoft.AspNetCore.Mvc;
using Moq;
using NorthwindEmployeeAPI.Controllers;
using NorthwindEmployeeAPI.Models;
using NorthwindEmployeeAPI.Services;

namespace NorthwindEmployeeAPITests;

public class EmployeeDeleteTests
{
    [Category("Happy Path")]
    [Category("DeleteEmployee")]
    [Test]
    public async Task DeleteEmployee_GivenAValidId_ReturnsNoContent()
    {
        var mockEmployeeService = Mock.Of<INorthwindService<Employee>>();
        var mockTerritoryService = Mock.Of<INorthwindService<Territory>>();
        var mockOrderService = Mock.Of<IOrderService<Order>>();
        Employee employee = new Employee();
        int id = It.IsAny<int>();
        Mock
            .Get(mockEmployeeService)
            .Setup(sc => sc.DeleteAsync(id).Result)
            .Returns(true);
        Mock
            .Get(mockEmployeeService)
            .Setup(sc => sc.GetAsync(id).Result)
            .Returns(employee);

        var sut = new EmployeesController(mockEmployeeService, mockOrderService, mockTerritoryService);
        var result = await sut.DeleteEmployee(id);
        Assert.That(result, Is.TypeOf<NoContentResult>());
    }

    [Category("Sad Path")]
    [Category("DeleteEmployee")]
    [Test]
    public async Task DeleteEmployee_GivenAnInvalidId_ReturnsNotFound()
    {
        var mockEmployeeService = Mock.Of<INorthwindService<Employee>>();
        var mockTerritoryService = Mock.Of<INorthwindService<Territory>>();
        var mockOrderService = Mock.Of<IOrderService<Order>>();
        int id = It.IsAny<int>();
        Mock
            .Get(mockEmployeeService)
            .Setup(sc => sc.DeleteAsync(id).Result)
            .Returns(false);
        Mock
            .Get(mockEmployeeService)
            .Setup(sc => sc.GetAsync(id).Result)
            .Returns((Employee)null);

        var sut = new EmployeesController(mockEmployeeService, mockOrderService, mockTerritoryService);
        var result = await sut.DeleteEmployee(id);
        Assert.That(result, Is.TypeOf<NotFoundResult>());
    }

    [Category("Happy Path")]
    [Category("DeleteEmployeeTerritory")]
    [Test]
    public async Task DeleteEmployeeTerritory_GivenValidEmployeeIdAndTerritoryId_ReturnsNoContent()
    {
        var mockEmployeeService = Mock.Of<INorthwindService<Employee>>();
        var mockTerritoryService = Mock.Of<INorthwindService<Territory>>();
        var mockOrderService = Mock.Of<IOrderService<Order>>();
        Employee employee = Mock.Of<Employee>();
        Territory territory = Mock.Of<Territory>();
        int employeeId = It.IsAny<int>();
        string territoryId = It.IsAny<string>();
        Mock
            .Get(mockEmployeeService)
            .Setup(sc => sc.GetAsync(employeeId).Result)
            .Returns(employee);
        Mock
            .Get(mockTerritoryService)
            .Setup(sc => sc.GetAsync(territoryId).Result)
            .Returns(territory);
        Mock
            .Get(employee)
            .Setup(sc => sc.Territories.Remove(territory))
            .Returns(true);
        Mock
            .Get(territory)
            .Setup(sc => sc.Employees.Remove(employee))
            .Returns(true);

        var sut = new EmployeesController(mockEmployeeService, mockOrderService, mockTerritoryService);
        var result = await sut.DeleteEmployeeTerritory(employeeId, territoryId);
        Assert.That(result, Is.TypeOf<NoContentResult>());
    }

    [Category("Sad Path")]
    [Category("DeleteEmployeeTerritory")]
    [Test]
    public async Task DeleteEmployeeTerritory_GivenInvalidId_ReturnsNotFound()
    {
        var mockEmployeeService = Mock.Of<INorthwindService<Employee>>();
        var mockTerritoryService = Mock.Of<INorthwindService<Territory>>();
        var mockOrderService = Mock.Of<IOrderService<Order>>();
        Employee employee = Mock.Of<Employee>();
        Territory territory = Mock.Of<Territory>();
        int employeeId = It.IsAny<int>();
        string territoryId = It.IsAny<string>();
        Mock
            .Get(mockEmployeeService)
            .Setup(sc => sc.GetAsync(employeeId).Result)
            .Returns(employee);
        Mock
            .Get(mockTerritoryService)
            .Setup(sc => sc.GetAsync(territoryId).Result)
            .Returns((Territory)null);
        Mock
            .Get(employee)
            .Setup(sc => sc.Territories.Remove(territory))
            .Returns(true);
        Mock
            .Get(territory)
            .Setup(sc => sc.Employees.Remove(employee))
            .Returns(true);

        var sut = new EmployeesController(mockEmployeeService, mockOrderService, mockTerritoryService);
        var result = await sut.DeleteEmployeeTerritory(employeeId, territoryId);
        Assert.That(result, Is.TypeOf<NotFoundResult>());
    }

    [Category("Sad Path")]
    [Category("DeleteEmployeeTerritory")]
    [Test]
    public async Task DeleteEmployeeTerritory_GivenCannotRemoveFromLists_ReturnsNoContent()
    {
        var mockEmployeeService = Mock.Of<INorthwindService<Employee>>();
        var mockTerritoryService = Mock.Of<INorthwindService<Territory>>();
        var mockOrderService = Mock.Of<IOrderService<Order>>();
        Employee employee = Mock.Of<Employee>();
        Territory territory = Mock.Of<Territory>();
        int employeeId = It.IsAny<int>();
        string territoryId = It.IsAny<string>();
        Mock
            .Get(mockEmployeeService)
            .Setup(sc => sc.GetAsync(employeeId).Result)
            .Returns(employee);
        Mock
            .Get(mockTerritoryService)
            .Setup(sc => sc.GetAsync(territoryId).Result)
            .Returns(territory);
        Mock
            .Get(employee)
            .Setup(sc => sc.Territories.Remove(territory))
            .Returns(false);
        Mock
            .Get(territory)
            .Setup(sc => sc.Employees.Remove(employee))
            .Returns(true);

        var sut = new EmployeesController(mockEmployeeService, mockOrderService, mockTerritoryService);
        var result = await sut.DeleteEmployeeTerritory(employeeId, territoryId);
        Assert.That(result, Is.TypeOf<NotFoundResult>());
    }
}
