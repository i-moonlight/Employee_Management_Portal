using System.Collections;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WebAPI.Controllers;
using WebAPI.Tests.Common;
using WebAPI.UseCases.Common.Dto.Auth;
using WebAPI.UseCases.Common.Dto.Response;

namespace WebAPI.Tests.Controllers
{
    [TestFixture]
    public class AuthControllerTests : ControllerTestSetup
    {
        private AuthController _authController;

        [SetUp]
        public new void Setup()
        {
            _authController = new AuthController();
        }

        [Test]
        public void GetUserList_Method_Should_Returns_ActionResult_IEnumerable_Type()
        {
            // Act.
            var result = _authController.GetUserList();

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<IEnumerable>>), result.GetType());
        }

        [Test]
        public async Task GetUserList_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Act.
            var response = await HttpClient.GetAsync("api/auth/GetAllUsers");

            // Assert.
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void RegisterUser_Method_Should_Returns_ActionResult_ResponseModel_Type()
        {
            // Arrange.
            var registerUser = new RegisterUserDto();

            // Act.
            var result = _authController.RegisterUser(registerUser);

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<ResponseModel>>), result.GetType());
        }

        [Test]
        public async Task RegisterUser_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var content = FakeTestContent.GetRequestContent(FakeRegisterUserDto);

            // Act.
            var response = await HttpClient.PostAsync("api/auth/RegisterUser", content);

            // Assert.
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void SignIn_Method_Should_Returns_ActionResult_ResponseModel_Type()
        {
            // Arrange.
            var testLoginDto = FakeTestContent.FakeLoginDto;

            // Act.
            var result = _authController.SignIn(testLoginDto);

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<ResponseModel>>), result.GetType());
        }

        [Test]
        public async Task SignIn_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var content = FakeTestContent.GetRequestContent(FakeLoginDto);

            // Act.
            var response = await HttpClient.PostAsync("api/auth/SignIn", content);

            // Assert.
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}