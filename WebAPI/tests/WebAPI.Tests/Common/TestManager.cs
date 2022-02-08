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

        public static Mock<UserManager<TUser>> MockUserManagerCheckEmail<TUser>() where TUser : class, new()
        {
            var mockStore = new Mock<IUserPasswordStore<TUser>>();

            var mockUserManager =
                new Mock<UserManager<TUser>>(mockStore.Object, null, null, null, null, null, null, null, null);

            mockUserManager
                .Setup(m => m.FindByEmailAsync(TestContent.GetTestUser().Email))
                .ReturnsAsync(TestContent.GetTestUser() as TUser);

            mockUserManager
                .Setup(m => m.GetRolesAsync(It.IsAny<TUser>()))
                .ReturnsAsync(Mock.Of<IList<string>>());

            mockUserManager
                .Setup(m => m.GenerateUserTokenAsync(It.IsAny<TUser>(),It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("token");
            
            return mockUserManager;
        }

        public static Mock<SignInManager<TUser>> MockSignInManager<TUser>() where TUser : class
        {
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>();
            var mockUserManager = MockUserManagerCheckEmail<User>();

            var mockSignInManager = new Mock<SignInManager<TUser>>(mockUserManager.Object, contextAccessor.Object,
                userPrincipalFactory.Object, null, null, null, null);
            
            mockSignInManager
                .Setup(m => m.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), true, false))
                .ReturnsAsync(SignInResult.Success);

            return mockSignInManager;
        }
    }
}