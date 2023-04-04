using Microsoft.EntityFrameworkCore;
using NorthwindEmployeeAPI.Controllers;
using NorthwindEmployeeAPI.Data.Repository;
using NorthwindEmployeeAPI.Models;

namespace NorthwindEmployeeReadTests;

public class Tests
{
    private EmployeeRepository _sut;
    private NorthwindContext _context;
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        var options = new DbContextOptionsBuilder<NorthwindContext>()
        .UseInMemoryDatabase("NorthwindDB").Options;
        _context = new NorthwindContext(options);
        _sut = new EmployeeRepository(_context);
    }
    [SetUp]
    public void Setup()
    {
        if (_context.Employees != null)
        {
            _context.Employees.RemoveRange(_context.Employees);
        }
        _context.Employees!.AddRange(
        new List<Employee>
        {
         new Employee
         {
         EmployeeId = 1,
         FirstName = "Ali",
         LastName = "ALI",
         Title = "Doctor",
         },
         new Employee {
         EmployeeId = 2,
         FirstName = "Alex",
         LastName = "ALEX",
         Title = "Master",
         }
                    });
        _context.SaveChanges();
    }
    [Category("happy path")]
    [Category("find async")]
    [Test]
    public void FindAsync_GivenValidID_ReturnsCorrectEmployee()
    {
        var result = _sut.FindAsync(1).Result;
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<Employee>());
        Assert.That(result.EmployeeId == 1);
        Assert.That(result.FirstName == "Ali");
    }
    [Category("sad path")]
    [Category("find async")]
    [Test]
    public void FindAsync_GivenInvalidID_ReturnsNull()
    {
        var result = _sut.FindAsync(1000).Result;
        Assert.That(result, Is.Null);
    }
    [Category("Happy path")]
    [Category("getallasync")]
    [Test]
    public void GetAllAsync_GivenEmployeesIsNotNull_ReturnsList()
    {
        var result = _sut.GetAllAsync().Result;
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<List<Employee>>());
    }
    [Category("sad path")]
    [Category("getallasync")]
    [Test]
    public void GetAllAsync_GivenEmployeesIsNull_ReturnsListOfCount0()
    {
        _context.Employees.RemoveRange(_context.Employees);
        _context.SaveChanges();
        var result = _sut.GetAllAsync().Result;
        Assert.That(result.Count() == 0);
    }
}