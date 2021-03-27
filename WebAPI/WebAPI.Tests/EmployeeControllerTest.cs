using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebAPI.Controllers;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Tests
{
    [TestFixture]
    public class EmployeeControllerTest
    {
        private Mock<ICrudRepository<Employee>> _mockEmpRepo;
        private EmployeeController _controller;
        private IEnumerable<Employee> _fakeCategories;

        [SetUp]
        public void Setup()
        {
            _mockEmpRepo = new Mock<ICrudRepository<Employee>>();
            _controller = new EmployeeController(_mockEmpRepo.Object);
            _fakeCategories = GetCategories();
        }

        [Test]
        public void Get_ShouldReturnAllEmployees()
        {
            // Arrange   
            _mockEmpRepo.Setup(x => x.Read()).Returns(_fakeCategories);

            // Act
            JsonResult result = _controller.Get();

            // Assert
            Assert.NotNull(result, "Result is null");
            Assert.AreEqual(new JsonResult(new Employee()).GetType(), result.GetType(), "Return type mismatch");
            Assert.AreEqual(typeof(List<Employee>), result.Value.GetType(), "Return value type mismatch");
        }

        private static IEnumerable<Employee> GetCategories()
        {
            var fakeCategories = new List<Employee>
            {
                new Employee {EmployeeId = 1, EmployeeName = "Test1"}
            }.AsEnumerable();
            return fakeCategories;
        }
    }
}