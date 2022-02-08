using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using WebAPI.Entities.Models;
using WebAPI.Tests.Common;
using WebAPI.UserCases.Requests.Authentication.Commands;

namespace WebAPI.Tests.Requests.Commands
{
    [TestFixture]
    public class AuthCommandHandlersTests : RequestTestSetup
    {
        [Test]
        public async Task RegisterUserCommandHandler_Handle_Method_Should_Returns_Success_String()
        {
            // Arrange.
            var testRegisterUserDto = TestContent.GetTestRegisterUserDto();
            var request = new RegisterUserCommand() {RegisterUserDto = testRegisterUserDto};
            var manager = TestManager.MockUserManager<User>();
            var handler = new RegisterUserCommandHandler(manager.Object, Mapper);

            // Act.
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert.
            Assert.AreEqual("User has been registered", result.ResponseMessage);
        }
        
        [Test]
        public async Task RegisterUserCommandHandler_Handle_Method_Should_Returns_Failure_String()
        {
            // Arrange.
            var testRegisterUserDto = TestContent.GetTestRegisterUserDto();
            var request = new RegisterUserCommand() {RegisterUserDto = testRegisterUserDto};
            var manager = TestManager.MockUserManager<User>();
            
            manager
                .Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());
            
            var handler = new RegisterUserCommandHandler(manager.Object, Mapper);

            // Act.
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert.
            Assert.AreEqual("User has been not registered", result.ResponseMessage);
        }
        
        [Test]
        public async Task RegisterUserCommandHandler_Handle_Method_Should_Returns_Exception()
        {
            // Arrange.
            var testRegisterUserDto = TestContent.GetTestRegisterUserDto();
            var request = new RegisterUserCommand() {RegisterUserDto = testRegisterUserDto};
            var manager = TestManager.MockUserManager<User>();
            
            manager
                .Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync((IdentityResult) null);
            
            var handler = new RegisterUserCommandHandler(manager.Object, Mapper);

            // Act.
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert.
            Assert.AreEqual("Object reference not set to an instance of an object.", result.ResponseMessage);
        }

        [Test]
        public async Task SignInCommandHandler_Handle_Method_Should_Returns_Token()
        {
            // Arrange.
            var testRegisterUserDto = TestContent.GetTestLoginDto();
            var request = new SignInCommand() {LoginDto = testRegisterUserDto};

            var mockUserManager = TestManager.MockUserManagerCheckEmail<User>();
            var mockSignInManager = TestManager.MockSignInManager<User>();

            var handler = new SignInCommandHandler(mockUserManager.Object, mockSignInManager.Object);

            // Act.
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert.
            Assert.AreEqual("Token generated.", result.ResponseMessage);
        }
    }
}