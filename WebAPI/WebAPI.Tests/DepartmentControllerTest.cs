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
        private Department _model;

        [SetUp]
        public void Setup()
        {
            _mockDepRepo = new Mock<ICrudRepository<Department>>();
            _controller = new DepartmentController(_mockDepRepo.Object);
            _fakeCategories = GetCategories();
            _model = new Department();
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
        
        [Test]
        public void Post_ShouldCreateDepartment()
        {
            // Arrange.   
            _mockDepRepo.Setup(x => x.Create(new Department())).Returns(_fakeCategories.GetEnumerator().Current);

            // Act.
            JsonResult result = _controller.Post(_model);

            // Assert.
            Assert.NotNull(result, "Result is null");
            Assert.AreEqual(new JsonResult(_model).GetType(), result.GetType(), "Return type mismatch");
            Assert.AreEqual(typeof(string), result.Value.GetType(), "Return value type mismatch");
        }    
        
        [Test]
        public void Put_ShouldUpdateDepartment()
        {
            // Arrange. 
            _mockDepRepo.Setup(x => x.Update(_model)).Returns(_fakeCategories.GetEnumerator().Current);

            // Act.
            JsonResult result = _controller.Put(_model);

            // Assert.
            Assert.NotNull(result, "Result is null");
            Assert.AreEqual(new JsonResult(_model).GetType(), result.GetType(), "Return type mismatch");
            Assert.AreEqual(typeof(string), result.Value.GetType(), "Return value type mismatch");
        }
        
        [Test]
        public void Delete_ShouldDeleteDepartment()
        {
            // Arrange.
            _mockDepRepo.Setup(x => x.Delete(_model.DepartmentId));

            // Act.
            JsonResult result = _controller.Delete(_model.DepartmentId);

            // Assert.
            Assert.NotNull(result, "Result is null");
            Assert.AreEqual(new JsonResult(_model).GetType(), result.GetType(), "Return type mismatch");
            Assert.AreEqual(typeof(string), result.Value.GetType(), "Return value type mismatch");
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