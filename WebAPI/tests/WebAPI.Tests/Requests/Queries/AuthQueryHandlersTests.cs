using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using WebAPI.Service.Authentication.Entities;
using WebAPI.Service.Authentication.UseCases.Commands;
using WebAPI.Tests.Common;

namespace WebAPI.Tests.Requests.Queries
{
    [TestFixture]
    public class AuthQueryHandlersTests
    {
        [Test]
        public async Task GetUserListQueryHandler_Handler_Method_Should_Returns_User_List()
        {
            // Arrange
            var request = new GetUserListQuery();
            var manager = MockInstances.GetMockUserManager<User>().Object;
            var handler = new GetUserListQueryHandler(manager);

            // Act.
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert.
            Assert.IsEmpty(result);
        }
    }
}