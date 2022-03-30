using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using WebAPI.Infrastructure.Interfaces.Options;
using WebAPI.Service.Authentication.Entities;
using WebAPI.Service.Authentication.UseCases.Commands;
using WebAPI.Service.Authentication.UseCases.Dto;
using WebAPI.Service.Authentication.UseCases.Services;
using WebAPI.Tests.Common;
using static WebAPI.Utils.Constants.MessageTypes;
using static System.Threading.CancellationToken;

namespace WebAPI.Tests.Requests.Commands
{
    [TestFixture]
    public class AuthCommandHandlersTests : RequestTestSetup
    {
        private Mock<EmailOptions> _mockEmailOptions;
        private Mock<JwtOptions> _mockJwtOptions;
        private Mock<UserManager<User>> _mockUserManager;
        private Mock<SignInManager<User>> _mockSignInManager;
        private RegisterUserDto _testRegisterUserDto;
        private Mock<IEmailService> _mockIEmailService;
        private Mock<IHttpContextAccessor> _mockIHttpContextAccessor;

        [SetUp]
        public new void Setup()
        {
            _mockEmailOptions = new Mock<EmailOptions>();
            _mockJwtOptions = new Mock<JwtOptions>();
            _mockUserManager = MockInstances.GetMockUserManager<User>();
            _mockSignInManager = MockInstances.GetMockSignInManager<User>();
            _testRegisterUserDto = FakeTestContent.FakeRegisterUserDto;
            _mockIEmailService = new Mock<IEmailService>();
            _mockIHttpContextAccessor = new Mock<IHttpContextAccessor>();
        }

        [Test]
        public async Task RegisterUserCommandHandler_Handle_Method_Should_Returns_Success_String()
        {
            // Arrange.
            var request = new RegisterUserCommand() { RegisterUserDto = _testRegisterUserDto };
            var handler = new RegisterUserCommandHandler(_mockUserManager.Object, Mapper);

            // Act.
            var result = await handler.Handle(request, None);

            // Assert.
            Assert.AreEqual(RegistrationSuccess, result.Message);
        }

        [Test]
        public async Task RegisterUserCommandHandler_Handle_Method_Should_Returns_Failure_String()
        {
            // Arrange.
            var request = new RegisterUserCommand() { RegisterUserDto = _testRegisterUserDto };

            _mockUserManager
                .Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

            var handler = new RegisterUserCommandHandler(_mockUserManager.Object, Mapper);

            // Act.
            var result = await handler.Handle(request, None);

            // Assert.
            Assert.AreEqual(RegistrationFailed, result.Message);
        }

        [Test]
        public async Task RegisterUserCommandHandler_Handle_Method_Should_Returns_Exception()
        {
            // Arrange.
            var request = new RegisterUserCommand() { RegisterUserDto = _testRegisterUserDto };

            _mockUserManager
                .Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync((IdentityResult)null);

            var handler = new RegisterUserCommandHandler(_mockUserManager.Object, Mapper);

            // Act.
            var result = await handler.Handle(request, None);

            // Assert.
            Assert.AreEqual(NullReference, result.Message);
        }

        // [Test]
        // public async Task SignInCommandHandler_Handle_Method_Should_Returns_Invalid_Result()
        // {
        //     // Arrange.
        //     var fakeDto = FakeTestContent.FakeLoginDto;
        //     var request = new SignInCommand() { LoginDto = fakeDto };
        //     var handler = new SignInCommandHandler(_mockUserManager.Object, _mockSignInManager.Object,
        //         _mockJwtOptions.Object);
        //
        //     _mockSignInManager
        //         .Setup(m => m.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), true, false))
        //         .ReturnsAsync(SignInResult.Failed);
        //
        //     // Act.
        //     var result = await handler.Handle(request, None);
        //
        //     // Assert.
        //     Assert.AreEqual(InvalidEmailOrPassword, result.Message);
        // }
        //
        // [Test]
        // public async Task SignInCommandHandler_Handle_Method_Should_Returns_Exception()
        // {
        //     // Arrange.
        //     var request = new SignInCommand() { LoginDto = null };
        //     var handler = new SignInCommandHandler(_mockUserManager.Object, _mockSignInManager.Object,
        //         _mockJwtOptions.Object);
        //
        //     // Act.
        //     var result = await handler.Handle(request, None);
        //
        //     // Assert.
        //     Assert.AreEqual(NullReference, result.Message);
        // }
        //
        // [Test]
        // public async Task ForgotPasswordCommandHandler_Handle_Method_Should_Returns_Invalid_Result()
        // {
        //     // Arrange.
        //     var fakeDto = FakeTestContent.FakeAccountDto;
        //     var request = new ForgotPasswordCommand() { AccountDto = fakeDto };
        //     var handler = new ForgotPasswordCommandHandler(_mockUserManager.Object, _mockIEmailService.Object,
        //         _mockIHttpContextAccessor.Object, _mockEmailOptions.Object);
        //
        //     _mockUserManager
        //         .Setup(m => m.FindByEmailAsync(It.IsAny<string>()))
        //         .Returns(Task.FromResult(new User { UserName = "testName" }));
        //
        //     _mockUserManager
        //         .Setup(m => m.GeneratePasswordResetTokenAsync(It.IsAny<User>()))
        //         .Returns(Task.FromResult(FakeTestContent.FakeToken));
        //
        //     var context = new DefaultHttpContext();
        //     _mockIHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);
        //
        //     // Act.
        //     var result = await handler.Handle(request, None);
        //
        //     // Assert.
        //     Assert.AreEqual("Value cannot be null. (Parameter 'uriString')", result.Message);
        // }
    }
}