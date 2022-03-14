using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using WebAPI.Entities.Models;
using WebAPI.Tests.Common;
using WebAPI.UseCases.Common.Dto;
using WebAPI.UseCases.Common.Exceptions;
using WebAPI.UseCases.Requests.Employees.Queries;
using static System.Threading.CancellationToken;

namespace WebAPI.Tests.Requests.Queries
{
    [TestFixture]
    public class EmployeeQueryHandlersTests : RequestTestSetup
    {
        private GetEmployeeQuery _request;
        private Employee _fakeEmployee;

        [SetUp]
        public new void Setup()
        {
            _request = new GetEmployeeQuery();
            _fakeEmployee = FakeTestContent.FakeEmployeeList.Cast<Employee>().First();
            _request.Id = _fakeEmployee.Id;
        }

        [Test]
        public async Task GetEmployeeListQueryHandler_Handler_Method_Should_Returns_Employee_List()
        {
            // Arrange.
            var handler = new GetEmployeeListQueryHandler(MockEmployeeRepo.Object);
            var employeeList = FakeTestContent.FakeEmployeeList;

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
            MockEmployeeRepo.Setup(r => r.Read(_request.Id)).Returns(_fakeEmployee);

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

            var fakeDepartmentNameList = FakeTestContent.FakeDepartmentList
                .Cast<Department>()
                .OrderBy(d => d.Name).Select(d => d.Name)
                .ToList();

            MockEmployeeRepo.Setup(r => r.ReadAll()).Returns(fakeDepartmentNameList);

            // Act.
            var result = await handler.Handle(new GetDepartmentNameListQuery(), None);

            // Assert.
            Assert.AreEqual(typeof(List<string>), result.GetType());
        }
    }
}