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
    public class DepartmentControllerTest
    {
        private Mock<ICrudRepository<Department>> _mockDepRepo;
        private DepartmentController _controller;
        private IEnumerable<Department> _fakeCategories;

        [SetUp]
        public void Setup()
        {
            _mockDepRepo = new Mock<ICrudRepository<Department>>();
            _controller = new DepartmentController(_mockDepRepo.Object);
            _fakeCategories = GetCategories();
        }

        [Test]
        public void Get_ShouldReturnAllDepartments()
        {
            // Arrange.   
            _mockDepRepo.Setup(x => x.Read()).Returns(_fakeCategories);

            // Act.
            JsonResult result = _controller.Get();

            // Assert.
            Assert.NotNull(result, "Result is null");
            Assert.AreEqual(new JsonResult(new Department()).GetType(), result.GetType(), "Return type mismatch");
            Assert.AreEqual(typeof(List<Department>), result.Value.GetType(), "Return value type mismatch");
        }

        private static IEnumerable<Department> GetCategories()
        {
            var fakeCategories = new List<Department>
            {
                new Department {DepartmentId = 1, DepartmentName = "Test1"}
            }.AsEnumerable();
            return fakeCategories;
        }
    }
}