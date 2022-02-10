using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using WebAPI.Entities.Models;
using WebAPI.Tests.Common;
using WebAPI.UserCases.Common.Dto;
using WebAPI.UserCases.Requests.Authentication.Commands;

namespace WebAPI.Tests.Requests.Commands
{
    [TestFixture]
    public class AuthCommandHandlersTests : RequestTestSetup
    {
        private Mock<UserManager<User>> _mockUserManager;
        private Mock<SignInManager<User>> _mockSignInManager;
        private RegisterUserDto _testRegisterUserDto;

        [SetUp]
        public new void Setup()
        {
            _mockUserManager = MockInstances.GetMockUserManager<User>();
            _mockSignInManager = MockInstances.GetMockSignInManager<User>();
            _testRegisterUserDto = TestContent.TestRegisterUserDto;
        }

        [Test]
        public async Task RegisterUserCommandHandler_Handle_Method_Should_Returns_Success_String()
        {
            // Arrange.
            var request = new RegisterUserCommand() {RegisterUserDto = _testRegisterUserDto};
            var handler = new RegisterUserCommandHandler(_mockUserManager.Object, Mapper);

            // Act.
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert.
            Assert.AreEqual("User has been registered.", result.ResponseMessage);
        }

        [Test]
        public async Task RegisterUserCommandHandler_Handle_Method_Should_Returns_Failure_String()
        {
            // Arrange.
            var request = new RegisterUserCommand() {RegisterUserDto = _testRegisterUserDto};

            _mockUserManager
                .Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

            var handler = new RegisterUserCommandHandler(_mockUserManager.Object, Mapper);

            // Act.
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert.
            Assert.AreEqual("User has been not registered.", result.ResponseMessage);
        }

        [Test]
        public async Task RegisterUserCommandHandler_Handle_Method_Should_Returns_Exception()
        {
            // Arrange.
            var request = new RegisterUserCommand() {RegisterUserDto = _testRegisterUserDto};

            _mockUserManager
                .Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync((IdentityResult) null);

            var handler = new RegisterUserCommandHandler(_mockUserManager.Object, Mapper);

            // Act.
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert.
            Assert.AreEqual("Object reference not set to an instance of an object.", result.ResponseMessage);
        }

        [Test]
        public async Task SignInCommandHandler_Handle_Method_Should_Returns_Token()
        {
            // Arrange.
            var testRegisterUserDto = TestContent.TestLoginDto;
            var request = new SignInCommand() {LoginDto = testRegisterUserDto};
            var handler = new SignInCommandHandler(_mockUserManager.Object, _mockSignInManager.Object);

            // Act.
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert.
            Assert.AreEqual("Token generated.", result.ResponseMessage);
        }

        [Test]
        public async Task SignInCommandHandler_Handle_Method_Should_Returns_Exception()
        {
            // Arrange.
            var request = new SignInCommand() {LoginDto = null};
            var handler = new SignInCommandHandler(_mockUserManager.Object, _mockSignInManager.Object);

            // Act.
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert.
            Assert.AreEqual("Object reference not set to an instance of an object.", result.ResponseMessage);
        }
    }
}