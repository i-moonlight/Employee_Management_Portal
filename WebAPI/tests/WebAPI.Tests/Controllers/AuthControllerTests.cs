using System.Collections;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WebAPI.Tests.Common;

namespace WebAPI.Tests.Controllers
{
    [TestFixture]
    public class AuthControllerTests : ControllerTestSetup
    {
        [Test]
        public void GetUserList_Method_Should_Returns_ActionResult_IEnumerable_Type()
        {
            // Act.
            var result = AuthController.GetUserList();

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<IEnumerable>>), result.GetType());
        }

        [Test]
        public async Task GetUserList_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Act.
            var response = await HttpClient.GetAsync("api/auth/GetAllUsers");

            // Assert.
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}