﻿using System.Collections;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WebAPI.Service.Authentication.Controllers;
using WebAPI.Service.Authentication.Tests.Common;
using WebAPI.Service.Authentication.UseCases.Dto;

namespace WebAPI.Service.Authentication.Tests.Controllers
{
    [TestFixture]
    public class AuthControllerTests : ControllerTestSetup
    {
        private AuthController _authController;

        [SetUp]
        public new void Setup() => _authController = new AuthController();

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
        public void SignUp_Method_Should_Returns_ActionResult_ResponseModel_Type()
        {
            // Arrange.
            var registerUser = new RegisterUserDto();

            // Act.
            var result = _authController.SignUp(registerUser);

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<ResponseModel>>), result.GetType());
        }

        [Test]
        public async Task SignUp_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var content = FakeAuthContent.GetRequestContent(FakeRegisterUserDto);

            // Act.
            var response = await HttpClient.PostAsync("api/auth/SignUp", content);

            // Assert.
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void SignIn_Method_Should_Returns_ActionResult_ResponseModel_Type()
        {
            // Arrange.
            var testLoginDto = FakeAuthContent.FakeLoginDto;

            // Act.
            var result = _authController.SignIn(testLoginDto);

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<ResponseModel>>), result.GetType());
        }

        [Test]
        public async Task SignIn_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var content = FakeAuthContent.GetRequestContent(FakeLoginDto);

            // Act.
            var response = await HttpClient.PostAsync("api/auth/SignIn", content);

            // Assert.
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void ForgotPassword_Method_Should_Returns_ActionResult_ResponseModel_Type()
        {
            // Arrange.
            var fakeDto = FakeAuthContent.FakeAccountDto;

            // Act.
            var result = _authController.ForgotPassword(fakeDto);

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<ResponseModel>>), result.GetType());
        }

        [Test]
        public async Task ForgotPassword_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var fakeDto = FakeAuthContent.FakeAccountDto;
            var content = FakeAuthContent.GetRequestContent(fakeDto);

            // Act.
            var response = await HttpClient.PostAsync("api/auth/ForgotPassword", content);

            // Assert.
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}