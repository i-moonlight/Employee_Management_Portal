using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebAPI.Domain.Core.Entities;
using WebAPI.Domain.Core.Interfaces;
using WebAPI.Presentation.Controllers;

namespace WebAPI.Tests
{
    [TestFixture]
    public class EmployeeControllerTest
    {
        private Mock<ICrudRepository<Employee>> _mockEmpRepo;
        private EmployeeController _controller;
        private IEnumerable<Employee> _fakeCategories;
        private Employee _model;
        private readonly IWebHostEnvironment _env = null;

        [SetUp]
        public void Setup()
        {
            _mockEmpRepo = new Mock<ICrudRepository<Employee>>();
            _controller = new EmployeeController(_mockEmpRepo.Object, _env);
            _fakeCategories = GetCategories();
            _model = new Employee();
        }

        [Test]
        public void Get_Should_Returns_JsonResult_String_Value()
        {
            // Arrange.   
            _mockEmpRepo.Setup(x => x.Read()).Returns(_fakeCategories);

            // Act.
            JsonResult result = _controller.Get();

            // Assert.
            Assert.NotNull(result, "Result is null");
            Assert.AreEqual(new JsonResult(new Employee()).GetType(), result.GetType(), "Return type mismatch");
            Assert.AreEqual(typeof(List<Employee>), result.Value.GetType(), "Return value type mismatch");
        }
        
        [Test]
        public void Post_Should_Returns_JsonResult_String_Value()
        {
            // Arrange.   
            _mockEmpRepo.Setup(x => x.Create(_model)).Returns(_fakeCategories.GetEnumerator().Current);

            // Act.
            JsonResult result = _controller.Post(_model);

            // Assert.
            Assert.NotNull(result, "Result is null");
            Assert.AreEqual(new JsonResult(_model).GetType(), result.GetType(), "Return type mismatch");
            Assert.AreEqual(typeof(string), result.Value.GetType(), "Return value type mismatch");
        }
        
        [Test]
        public void Put_Should_Returns_JsonResult_String_Value()
        {
            // Arrange.
            _mockEmpRepo.Setup(x => x.Update(_model)).Returns(_fakeCategories.GetEnumerator().Current);

            // Act.
            JsonResult result = _controller.Put(_model);

            // Assert.
            Assert.NotNull(result, "Result is null");
            Assert.AreEqual(new JsonResult(_model).GetType(), result.GetType(), "Return type mismatch");
            Assert.AreEqual(typeof(string), result.Value.GetType(), "Return value type mismatch");
        }
        
        [Test]
        public void Delete_Should_Returns_JsonResult_String_Value()
        {
            // Arrange.   
            _mockEmpRepo.Setup(x => x.Delete(_model.EmployeeId));

            // Act.
            JsonResult result = _controller.Delete(_model.EmployeeId);

            // Assert.
            Assert.NotNull(result, "Result is null");
            Assert.AreEqual(new JsonResult(_model).GetType(), result.GetType(), "Return type mismatch");
            Assert.AreEqual(typeof(string), result.Value.GetType(), "Return value type mismatch");
        }
        
        [Test]
        public void UploadPhoto_Should_Returns_JsonResult_String_Value()
        {
            // Act.
            JsonResult result = _controller.UploadPhoto();

            // Assert.
            Assert.NotNull(result, "Result is null");
            Assert.AreEqual(new JsonResult(_model).GetType(), result.GetType(), "Return type mismatch");
            Assert.AreEqual(typeof(string), result.Value.GetType(), "Return value type mismatch");
        }
        
        [Test]
        public void UpdatePhoto_Should_Returns_JsonResult_String_Value()
        {
            // Arrange.   
            _mockEmpRepo.Setup(x => x.GetFileName(_model.EmployeeId)).Returns("PhotoFileName");

            // Act.
            JsonResult result = _controller.UpdatePhoto(_model.EmployeeId);

            // Assert.
            Assert.NotNull(result, "Result is null");
            Assert.AreEqual(new JsonResult(_model).GetType(), result.GetType(), "Return type mismatch");
            Assert.AreEqual(typeof(string), result.Value.GetType(), "Return value type mismatch");
        }

        [Test]
        public void GetAllDepartmentNames_Should_Returns_ValueType_NotNull()
        {
            // Arrange.   
            _mockEmpRepo.Setup(x => x.ReadAll()).Returns(_fakeCategories);

            // Act.
            JsonResult result = _controller.GetAllDepartmentNames();

            // Assert.
            Assert.NotNull(result, "Result is null");
            Assert.AreEqual(new JsonResult(_model).GetType(), result.GetType(), "Return type mismatch");
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