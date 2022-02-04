using Microsoft.AspNetCore.Identity;
using Moq;

namespace WebAPI.Tests.Common
{
    /// <summary>
    /// Sets access to mock objects for testing.
    /// </summary>
    public static class TestManager
    {
        public static Mock<UserManager<TUser>> MockUserManager<TUser>(IUserStore<TUser> store = null)
            where TUser : class
        {
            var mockStore = new Mock<IUserPasswordStore<TUser>>();

            var mockUserManager = new Mock<UserManager<TUser>>(
                mockStore.Object, null, null, null, null, null, null, null, null);

            mockUserManager
                .Setup(um => um.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            return mockUserManager;
        }
    }
}