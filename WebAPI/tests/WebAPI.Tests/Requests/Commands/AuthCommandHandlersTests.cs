using System.Threading;
using System.Threading.Tasks;
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
    }
}