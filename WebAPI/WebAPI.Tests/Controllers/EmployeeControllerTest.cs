using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WebAPI.Controllers;
using WebAPI.Domain.Entities;
using WebAPI.Tests.Setup;
using WebAPI.UseCases.Dto;
using WebAPI.Utils.Constants;

namespace WebAPI.Tests.Controllers
{
    [TestFixture]
    public class EmployeeControllerTests : ControllerTestSetup
    {
        private EmployeeController _controller;

        [SetUp]
        public new void Setup()
        {
            _controller = new EmployeeController();

            FakeDbContext.Employees.AddRangeAsync(new Employee());
            FakeDbContext.SaveChangesAsync();
        }

        [Test]
        public void GetEmployeeList_Method_Should_Returns_ActionResult_IEnumerable_Type()
        {
            // Act
            var result = _controller.GetEmployeeList();

            // Assert
            Assert.AreEqual(typeof(Task<ActionResult<IEnumerable>>), result.GetType());
        }

        [Test]
        public void GetEmployeeById_Method_Should_Returns_ActionResult_EmployeeDto_Type()
        {
            // Arrange
            var employeeId = FakeTestContent.FakeEmployeeDto.Id;

            // Act
            var result = _controller.GetEmployeeById(employeeId);

            // Assert
            Assert.AreEqual(typeof(Task<ActionResult<EmployeeDto>>), result.GetType());
        }

        [Test]
        public void GetDepartmentNameList_Method_Should_Returns_ActionResult_IEnumerable_Type()
        {
            // Act
            var result = _controller.GetDepartmentNameList();

            // Assert
            Assert.AreEqual(typeof(Task<ActionResult<IEnumerable>>), result.GetType());
        }

        [Test]
        public void CreateEmployee_Method_Should_Returns_ActionResult_String_Type()
        {
            // Act
            var result = _controller.CreateEmployee(FakeEmployeeDto);

            // Assert
            Assert.AreEqual(typeof(Task<ActionResult<string>>), result.GetType());
        }

        [Test]
        public async Task CreateEmployee_Method_Should_Returns_Validate_Response()
        {
            // Arrange
            var employeeDto = new EmployeeDto() {Name = null};
            var content = FakeTestContent.GetRequestContent(employeeDto);

            // Act
            var response = await HttpClient.PostAsync("api/employee", content);
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.AreEqual(ValidationTypes.NameMustFilled, stringResponse);
        }

        [Test]
        public void UploadEmployeePhoto_Method_Should_Returns_ActionResult_String_Type()
        {
            // Act
            var result = _controller.UploadEmployeePhoto();

            // Assert
            Assert.AreEqual(typeof(Task<ActionResult<string>>), result.GetType());
        }

        [Test]
        public void UpdateEmployeePhoto_Method_Should_Returns_ActionResult_String_Type()
        {
            // Arrange
            var employeeId = base.FakeEmployeeDto.Id;

            // Act
            var result = _controller.UpdateEmployeePhoto(employeeId);

            // Assert
            Assert.AreEqual(typeof(Task<ActionResult<string>>), result.GetType());
        }

        [Test]
        public void UpdateEmployee_Method_Should_Returns_ActionResult_String_Type()
        {
            // Act
            var result = _controller.UpdateEmployee(FakeEmployeeDto);

            // Assert
            Assert.AreEqual(typeof(Task<ActionResult<string>>), result.GetType());
        }

        [Test]
        public async Task UpdateEmployee_Method_Should_Returns_Validate_Response()
        {
            // Arrange
            var employeeDto = new EmployeeDto() {Name = null};
            var content = FakeTestContent.GetRequestContent(employeeDto);

            // Act
            var response = await base.HttpClient.PutAsync("api/employee", content);
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.AreEqual(ValidationTypes.NameMustFilled, stringResponse);
        }

        [Test]
        public void DeleteEmployee_Method_Should_Returns_ActionResult_String_Type()
        {
            // Arrange
            var employeeId = base.FakeEmployeeDto.Id;

            // Act
            var result = _controller.DeleteEmployeeById(employeeId);

            // Assert
            Assert.AreEqual(typeof(Task<ActionResult<string>>), result.GetType());
        }
    }
}
