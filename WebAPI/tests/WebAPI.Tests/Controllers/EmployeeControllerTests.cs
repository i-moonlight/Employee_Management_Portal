using NUnit.Framework;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Tests.Common;
using WebAPI.Entities.Models;
using WebAPI.UserCases.Common.Dto;
using static System.Net.HttpStatusCode;
using static WebAPI.Tests.Common.TestContent;

namespace WebAPI.Tests.Controllers
{
    [TestFixture]
    public class EmployeeControllerTests : ControllerTestSetup
    {
        [SetUp]
        public new void Setup()
        {
            TestDbContext.Employees.AddRangeAsync(new Employee());
            TestDbContext.SaveChangesAsync();
        }

        [Test]
        public void GetEmployeeList_Method_Should_Returns_ActionResult_IEnumerable_Type()
        {
            // Act.
            var result = EmployeeController.GetEmployeeList();

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<IEnumerable>>), result.GetType());
        }

        [Test]
        public async Task GetEmployeeList_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Act.
            var response = await HttpClient.GetAsync("api/employee");

            // Assert.
            Assert.AreEqual(OK, response.StatusCode);
        }

        [Test]
        public void GetEmployeeById_Method_Should_Returns_ActionResult_EmployeeDto_Type()
        {
            // Arrange.
            var employeeId = GetTestEmployeeDto().Id;

            // Act.
            var result = EmployeeController.GetEmployeeById(employeeId);

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<EmployeeDto>>), result.GetType());
        }

        [Test]
        public async Task GetEmployeeById_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var employeeId = TestDbContext.Employees?.FirstOrDefault()?.Id;

            // Act.
            var response = await HttpClient.GetAsync($"api/employee/{employeeId}");

            // Assert.
            Assert.AreEqual(OK, response.StatusCode);
        }

        [Test]
        public void GetDepartmentNameList_Method_Should_Returns_ActionResult_IEnumerable_Type()
        {
            // Act.
            var result = EmployeeController.GetDepartmentNameList();

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<IEnumerable>>), result.GetType());
        }

        [Test]
        public async Task GetDepartmentNameList_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Act.
            var response = await HttpClient.GetAsync("api/employee/GetDepartmentNames");

            // Assert.
            Assert.AreEqual(OK, response.StatusCode);
        }

        [Test]
        public void CreateEmployee_Method_Should_Returns_ActionResult_String_Type()
        {
            // Act.
            var result = EmployeeController.CreateEmployee(TestEmployeeDto);

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<string>>), result.GetType());
        }

        [Test]
        public async Task CreateEmployee_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var content = GetRequestContent(TestEmployeeDto);

            // Act.
            var response = await HttpClient.PostAsync("api/employee", content);
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert.
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("Created successfully", stringResponse);
        }

        [Test]
        public async Task CreateEmployee_Method_Should_Returns_Validate_Response()
        {
            // Arrange.
            var employeeDto = new EmployeeDto() {Name = null};
            var content = GetRequestContent(employeeDto);

            // Act.
            var response = await HttpClient.PostAsync("api/employee", content);
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert.
            Assert.AreEqual("Name must be filled", stringResponse);
        }

        [Test]
        public void UploadEmployeePhoto_Method_Should_Returns_ActionResult_String_Type()
        {
            // Act.
            var result = EmployeeController.UploadEmployeePhoto();

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<string>>), result.GetType());
        }

        [Test]
        public async Task UploadEmployeePhoto_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var content = GetRequestContent(TestEmployeeDto);

            // Act.
            var response = await HttpClient.PostAsync("api/employee/UploadPhoto", content);

            // Assert.
            Assert.AreEqual(OK, response.StatusCode);
        }

        [Test]
        public void UpdateEmployeePhoto_Method_Should_Returns_ActionResult_String_Type()
        {
            // Arrange.
            var employeeId = TestEmployeeDto.Id;

            // Act.
            var result = EmployeeController.UpdateEmployeePhoto(employeeId);

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<string>>), result.GetType());
        }

        [Test]
        public async Task UpdateEmployeePhoto_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var content = GetRequestContent(TestEmployeeDto);

            // Act.
            var response = await HttpClient.PostAsync("api/employee/UpdatePhoto", content);

            // Assert.
            Assert.AreEqual(OK, response.StatusCode);
        }

        [Test]
        public void UpdateEmployee_Method_Should_Returns_ActionResult_String_Type()
        {
            // Act.
            var result = EmployeeController.UpdateEmployee(TestEmployeeDto);

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<string>>), result.GetType());
        }

        [Test]
        public async Task UpdateEmployee_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var content = GetRequestContent(TestEmployeeDto);

            // Act.
            var response = await HttpClient.PutAsync("api/employee", content);
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert.
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("Updated successfully", stringResponse);
        }

        [Test]
        public async Task UpdateEmployee_Method_Should_Returns_Validate_Response()
        {
            // Arrange.
            var employeeDto = new EmployeeDto() {Name = null};
            var content = GetRequestContent(employeeDto);

            // Act.
            var response = await HttpClient.PutAsync("api/employee", content);
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert.
            Assert.AreEqual("Name must be filled", stringResponse);
        }

        [Test]
        public void DeleteEmployee_Method_Should_Returns_ActionResult_String_Type()
        {
            // Arrange.
            var employeeId = TestEmployeeDto.Id;

            // Act.
            var result = EmployeeController.DeleteEmployeeById(employeeId);

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<string>>), result.GetType());
        }

        [Test]
        public async Task DeleteEmployee_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var employeeId = TestDbContext.Employees?.FirstOrDefault()?.Id;

            // Act.
            var response = await HttpClient.DeleteAsync($"api/employee/{employeeId}");
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert.
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("Deleted successfully", stringResponse);
        }
    }
}