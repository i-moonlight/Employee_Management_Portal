using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebAPI.Application.Common.Interfaces;
using WebAPI.Controllers;
using WebAPI.Domain.Entities;

namespace WebAPI.Tests.Controllers
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
        public void Get_Should_Returns_JsonResult_String_Value()
        {
            // Arrange.   
            _mockDepRepo.Setup(x => x.Read()).Returns(_fakeCategories);

            // Act.
            var result = _controller.Get();

            // Assert.
            Assert.NotNull(result, "Result is null");
            Assert.AreEqual(new JsonResult(_model).GetType(), result.GetType(), "Return type mismatch");
            Assert.AreEqual(typeof(List<Department>), result.Value.GetType(), "Return value type mismatch");
        }
        
        [Test]
        public void Post_Should_Returns_JsonResult_String_Value()
        {
            // Arrange.   
            _mockDepRepo.Setup(x => x.Create(_model)).Returns(_fakeCategories.First());

            // Act.
            var result = _controller.Post(_fakeCategories.First());

            // Assert.
            Assert.NotNull(result, "Result is null");
            Assert.AreEqual(new JsonResult(_model).GetType(), result.GetType(), "Return type mismatch");
            Assert.AreEqual(typeof(string), result.Value.GetType(), "Return value type mismatch");
        }    
        
        [Test]
        public void Put_Should_Returns_JsonResult_String_Value()
        {
            // Arrange. 
            _mockDepRepo.Setup(x => x.Update(_model)).Returns(_fakeCategories.First());

            // Act.
            var result = _controller.Put(_fakeCategories.First());

            // Assert.
            Assert.NotNull(result, "Result is null");
            Assert.AreEqual(new JsonResult(_model).GetType(), result.GetType(), "Return type mismatch");
            Assert.AreEqual(typeof(string), result.Value.GetType(), "Return value type mismatch");
        }
        
        [Test]
        public void Delete_Should_Returns_JsonResult_String_Value()
        {
            // Arrange.
            _mockDepRepo.Setup(x => x.Delete(_fakeCategories.First().DepartmentId));

            // Act.
            var result = _controller.Delete(_fakeCategories.First().DepartmentId);

            // Assert.
            Assert.NotNull(result, "Result is null");
            Assert.AreEqual(new JsonResult(_model).GetType(), result.GetType(), "Return type mismatch");
            Assert.AreEqual(typeof(string), result.Value.GetType(), "Return value type mismatch");
        }

        private static IEnumerable<Department> GetCategories()
        {
            var fakeCategories = new List<Department>
                {
                    new Department
                    {
                        DepartmentId = 1,
                        DepartmentName = "Test"
                    }
                }
                .AsEnumerable();
            return fakeCategories;
        }
    }
}