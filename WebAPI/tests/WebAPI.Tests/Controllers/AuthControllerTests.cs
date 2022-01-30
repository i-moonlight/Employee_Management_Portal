using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WebAPI.Controllers;

namespace WebAPI.Tests.Controllers
{
    [TestFixture]
    public class AuthControllerTests
    {
        private AuthController _authController;

        [SetUp]
        public void Setup()
        {
            _authController = new AuthController();
        }

        [Test]
        public void GetUserList_Method_Should_Returns_ActionResult_IEnumerable_Type()
        {
            // Act.
            var result = _authController.GetUserList();

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<IEnumerable>>), result.GetType());
        }
    }
}