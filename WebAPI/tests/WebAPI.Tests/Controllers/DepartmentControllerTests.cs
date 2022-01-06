using System.Collections;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WebAPI.Controllers;
using WebAPI.Tests.Common;
using WebAPI.Web;

namespace WebAPI.Tests.Controllers
{
    [TestFixture]
    public class DepartmentControllerTests
    {
        private DepartmentController _controller;
        private TestWebApplicationFactory<Startup> _factory;

        [SetUp]
        public void Setup()
        {
            _controller = new DepartmentController();
            _factory = new TestWebApplicationFactory<Startup>();
        }

        [Test]
        public void GetDepartmentList_Method_Should_Returns_ActionResult_IEnumerable_Type()
        {
            // Act.
            var result = _controller.GetDepartmentList();

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<IEnumerable>>), result.GetType());
        }

        [Test]
        public async Task GetDepartmentList_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var client = _factory.CreateClient();

            // Act.
            var response = await client.GetAsync("api/department");

            // Assert.
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        // [Test]
        // public void Post_Should_Returns_JsonResult_String_Value()
        // {
        //     // Arrange.   
        //     _mockDepRepo.Setup(x => x.Create(_model)).Returns(_fakeCategories.First());
        //
        //     // Act.
        //     var result = _controller.Post(_fakeCategories.First());
        //
        //     // Assert.
        //     Assert.NotNull(result, "Result is null");
        //     Assert.AreEqual(new JsonResult(_model).GetType(), result.GetType(), "Return type mismatch");
        //     Assert.AreEqual(typeof(string), result.Value.GetType(), "Return value type mismatch");
        // }    
        //
        // [Test]
        // public void Put_Should_Returns_JsonResult_String_Value()
        // {
        //     // Arrange. 
        //     _mockDepRepo.Setup(x => x.Update(_model)).Returns(_fakeCategories.First());
        //
        //     // Act.
        //     var result = _controller.Put(_fakeCategories.First());
        //
        //     // Assert.
        //     Assert.NotNull(result, "Result is null");
        //     Assert.AreEqual(new JsonResult(_model).GetType(), result.GetType(), "Return type mismatch");
        //     Assert.AreEqual(typeof(string), result.Value.GetType(), "Return value type mismatch");
        // }
        //
        // [Test]
        // public void Delete_Should_Returns_JsonResult_String_Value()
        // {
        //     // Arrange.
        //     _mockDepRepo.Setup(x => x.Delete(_fakeCategories.First().Id));
        //
        //     // Act.
        //     //var result = _controller.Delete(_fakeCategories.First().Id);
        //
        //     // Assert.
        //     // Assert.NotNull(result, "Result is null");
        //     // Assert.AreEqual(new JsonResult(_model).GetType(), result.GetType(), "Return type mismatch");
        //     // Assert.AreEqual(typeof(string), result.Value.GetType(), "Return value type mismatch");
        // }
        //
    }
}