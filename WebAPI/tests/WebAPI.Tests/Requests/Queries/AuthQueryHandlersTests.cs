using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using WebAPI.Entities.Models;
using WebAPI.Tests.Common;
using WebAPI.UserCases.Requests.Authentication.Queries;

namespace WebAPI.Tests.Requests.Queries
{
    [TestFixture]
    public class AuthQueryHandlersTests : RequestTestSetup
    {
        private GetUserListQuery _request;

        [SetUp]
        public new void Setup()
        {
            _request = new GetUserListQuery();
        }

        [Test]
        public async Task GetUserListQueryHandler_Handler_Method_Should_Returns_User_List()
        {
            // Arrange
            var manager = MockInstances.GetMockUserManager<User>().Object;
            var handler = new GetUserListQueryHandler(manager);

            // Act.
            var result = await handler.Handle(_request, CancellationToken.None);

            // Assert.
            Assert.IsEmpty(result);
        }
    }
}