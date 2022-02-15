using NUnit.Framework;
using System.Collections;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;
using WebAPI.Tests.Common;
using WebAPI.Entities.Models;
using WebAPI.UserCases.Common.Dto;
using static WebAPI.Tests.Common.TestContent;
using static WebAPI.Utils.Constants.MessageTypes;

namespace WebAPI.Tests.Controllers
{
    [TestFixture]
    public class EmployeeControllerTests : ControllerTestSetup
    {
        private EmployeeController _departmentController;
        
        [SetUp]
        public new void Setup()
        {
            _departmentController = new EmployeeController();
            
            TestDbContext.Employees.AddRangeAsync(new Employee());
            TestDbContext.SaveChangesAsync();
        }

        [Test]
        public void GetEmployeeList_Method_Should_Returns_ActionResult_IEnumerable_Type()
        {
            // Act.
            var result = _departmentController.GetEmployeeList();

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<IEnumerable>>), result.GetType());
        }

        [Test]
        public async Task GetEmployeeList_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Act.
            var response = await HttpClient.GetAsync("api/employee");

            // Assert.
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public void GetEmployeeById_Method_Should_Returns_ActionResult_EmployeeDto_Type()
        {
            // Arrange.
            var employeeId = TestContent.TestEmployeeDto.Id;

            // Act.
            var result = _departmentController.GetEmployeeById(employeeId);

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
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void GetDepartmentNameList_Method_Should_Returns_ActionResult_IEnumerable_Type()
        {
            // Act.
            var result = _departmentController.GetDepartmentNameList();

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<IEnumerable>>), result.GetType());
        }

        [Test]
        public async Task GetDepartmentNameList_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Act.
            var response = await HttpClient.GetAsync("api/employee/GetDepartmentNames");

            // Assert.
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void CreateEmployee_Method_Should_Returns_ActionResult_String_Type()
        {
            // Act.
            var result = _departmentController.CreateEmployee(TestEmployeeDto);

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
            Assert.AreEqual(CreatedSuccessfull, stringResponse);
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
            Assert.AreEqual(NameMustFilled, stringResponse);
        }

        [Test]
        public void UploadEmployeePhoto_Method_Should_Returns_ActionResult_String_Type()
        {
            // Act.
            var result = _departmentController.UploadEmployeePhoto();

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
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void UpdateEmployeePhoto_Method_Should_Returns_ActionResult_String_Type()
        {
            // Arrange.
            var employeeId = TestEmployeeDto.Id;

            // Act.
            var result = _departmentController.UpdateEmployeePhoto(employeeId);

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
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void UpdateEmployee_Method_Should_Returns_ActionResult_String_Type()
        {
            // Act.
            var result = _departmentController.UpdateEmployee(TestEmployeeDto);

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
            Assert.AreEqual(UpdatedSuccessfull, stringResponse);
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
            Assert.AreEqual(NameMustFilled, stringResponse);
        }

        [Test]
        public void DeleteEmployee_Method_Should_Returns_ActionResult_String_Type()
        {
            // Arrange.
            var employeeId = TestEmployeeDto.Id;

            // Act.
            var result = _departmentController.DeleteEmployeeById(employeeId);

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
            Assert.AreEqual(DeletedSuccessfull, stringResponse);
        }
    }
}