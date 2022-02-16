using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using WebAPI.Entities.Models;
using WebAPI.Tests.Common;
using WebAPI.UserCases.Common.Dto;
using WebAPI.UserCases.Common.Exceptions;
using WebAPI.UserCases.Requests.Employees.Queries;
using static System.Threading.CancellationToken;

namespace WebAPI.Tests.Requests.Queries
{
    [TestFixture]
    public class EmployeeQueryHandlersTests : RequestTestSetup
    {
        private GetEmployeeQuery _request;
        private Employee _testEmployee;

        [SetUp]
        public new void Setup()
        {
            _request = new GetEmployeeQuery();
            _testEmployee = TestContent.TestEmployeeList.Cast<Employee>().First();
            _request.Id = _testEmployee.Id;
        }

        [Test]
        public async Task GetEmployeeListQueryHandler_Handler_Method_Should_Returns_Employee_List()
        {
            // Arrange.
            var handler = new GetEmployeeListQueryHandler(MockEmployeeRepo.Object);
            var employeeList = TestContent.TestEmployeeList;

            MockEmployeeRepo.Setup(r => r.Read()).Returns(employeeList);

            // Act.
            var result = await handler.Handle(new GetEmployeeListQuery(), None);

            // Assert.
            Assert.AreEqual(typeof(List<Employee>), result.GetType());
        }

        [Test]
        public async Task GetEmployeeQueryHandler_Handler_Method_Should_Returns_EmployeeDto()
        {
            // Arrange.
            var handler = new GetEmployeeQueryHandler(MockEmployeeRepo.Object, Mapper);
            MockEmployeeRepo.Setup(r => r.Read(_request.Id)).Returns(_testEmployee);

            // Act.
            var result = await handler.Handle(_request, None);

            // Assert.
            Assert.AreEqual(typeof(EmployeeDto), result.GetType());
        }

        [Test]
        public async Task GetEmployeeQueryHandler_Handler_Should_Returns_Exception()
        {
            // Arrange.
            var handler = new GetEmployeeQueryHandler(MockEmployeeRepo.Object, Mapper);
            MockEmployeeRepo.Setup(r => r.Read(_request.Id)).Returns(null as Employee);

            // Act.
            async Task Exception() => await handler.Handle(_request, None);

            // Assert.
            await Task.FromResult(Assert.ThrowsAsync<NotFoundException>(Exception));
        }

        [Test]
        public async Task GetDepartmentNameListQueryHandler_Handler_Method_Should_Returns_Name_List()
        {
            // Arrange.
            var handler = new GetDepartmentNameListQueryHandler(MockEmployeeRepo.Object);

            var testDepartmentNameList = TestContent.TestDepartmentList
                .Cast<Department>()
                .OrderBy(d => d.Name).Select(d => d.Name)
                .ToList();

            MockEmployeeRepo.Setup(r => r.ReadAll()).Returns(testDepartmentNameList);

            // Act.
            var result = await handler.Handle(new GetDepartmentNameListQuery(), None);

            // Assert.
            Assert.AreEqual(typeof(List<string>), result.GetType());
        }
    }
}