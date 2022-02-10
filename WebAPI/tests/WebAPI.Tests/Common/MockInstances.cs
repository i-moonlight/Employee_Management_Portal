using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using WebAPI.Entities.Models;

namespace WebAPI.Tests.Common
{
    /// <summary>
    /// Sets access to mock objects for testing.
    /// </summary>
    public static class MockInstances
    {
        public static Mock<UserManager<TUser>> GetMockUserManager<TUser>() where TUser : class
        {
            var mockStore = new Mock<IUserPasswordStore<TUser>>();

            var mockUserManager = new Mock<UserManager<TUser>>(mockStore.Object,
                null, null, null, null, null, null, null, null);

            mockUserManager
                .Setup(m => m.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            mockUserManager
                .Setup(m => m.FindByEmailAsync(TestContent.TestUser.Email))
                .ReturnsAsync(TestContent.TestUser as TUser);

            mockUserManager
                .Setup(m => m.GetRolesAsync(It.IsAny<TUser>()))
                .ReturnsAsync(Mock.Of<IList<string>>());

            mockUserManager
                .Setup(m => m.GenerateUserTokenAsync(It.IsAny<TUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("token");

            return mockUserManager;
        }

        public static Mock<SignInManager<TUser>> GetMockSignInManager<TUser>() where TUser : class
        {
            var mockUserManager = GetMockUserManager<User>();
            var mockContextAccessor = new Mock<IHttpContextAccessor>();
            var mockPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>();

            var mockSignInManager = new Mock<SignInManager<TUser>>(mockUserManager.Object, mockContextAccessor.Object,
                mockPrincipalFactory.Object, null, null, null, null);

            mockSignInManager
                .Setup(m => m.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), true, false))
                .ReturnsAsync(SignInResult.Success);

            return mockSignInManager;
        }
    }
}