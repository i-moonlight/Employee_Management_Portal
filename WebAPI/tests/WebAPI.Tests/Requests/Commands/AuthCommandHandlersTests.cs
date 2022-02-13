using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using WebAPI.Entities.Models;
using WebAPI.Tests.Common;
using WebAPI.UserCases.Common.Dto;
using WebAPI.UserCases.Requests.Authentication.Commands;
using static WebAPI.Utils.Constants.MessageTypes;

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
            Assert.AreEqual(RegistrationSuccess, result.ResponseMessage);
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
            Assert.AreEqual(RegistrationFailed, result.ResponseMessage);
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
            Assert.AreEqual(NullReference, result.ResponseMessage);
        }

        [Test]
        public async Task SignInCommandHandler_Handle_Method_Should_Returns_Token()
        {
            // Arrange.
            var testLoginDto = TestContent.TestLoginDto;
            var request = new SignInCommand() {LoginDto = testLoginDto};
            var handler = new SignInCommandHandler(_mockUserManager.Object, _mockSignInManager.Object);

            // Act.
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert.
            Assert.AreEqual(TokenGenerated, result.ResponseMessage);
        }

        [Test]
        public async Task SignInCommandHandler_Handle_Method_Should_Returns_Invalid_Result()
        {
            // Arrange.
            var testLoginDto = TestContent.TestLoginDto;
            var request = new SignInCommand() {LoginDto = testLoginDto};
            var handler = new SignInCommandHandler(_mockUserManager.Object, _mockSignInManager.Object);

            _mockSignInManager
                .Setup(m => m.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), true, false))
                .ReturnsAsync(SignInResult.Failed);

            // Act.
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert.
            Assert.AreEqual(InvalidEmailOrPassword, result.ResponseMessage);
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
            Assert.AreEqual(NullReference, result.ResponseMessage);
        }
    }
}