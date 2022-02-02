using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using WebAPI.Entities.Models;
using WebAPI.Tests.Common;
using WebAPI.UserCases.Common.Dto;
using WebAPI.UserCases.Common.Exceptions;
using WebAPI.UserCases.Requests.Departments.Queries;
using static System.Threading.CancellationToken;
using static WebAPI.Tests.Common.TestContent;

namespace WebAPI.Tests.Requests.Queries
{
    [TestFixture]
    public class DepartmentQueryHandlersTests : RequestTestSetup
    {
        private GetDepartmentQuery _request;
        private GetDepartmentQueryHandler _handler;
        private IEnumerable _testDepartmentList;

        [SetUp]
        public new void Setup()
        {
            _request = new GetDepartmentQuery();
            _handler = new GetDepartmentQueryHandler(MockDepartmentRepo.Object, Mapper);
            _testDepartmentList = GetTestDepartmentList();
        }

        [Test]
        public async Task GetDepartmentListQueryHandler_Handler_Method_Should_Returns_Department_List()
        {
            // Arrange.
            var handler = new GetDepartmentListQueryHandler(MockDepartmentRepo.Object);
            MockDepartmentRepo.Setup(r => r.Read()).Returns(_testDepartmentList);

            // Act.
            var result = await handler.Handle(new GetDepartmentListQuery(), None);

            // Assert.
            Assert.AreEqual(typeof(List<Department>), result.GetType());
        }

        [Test]
        public async Task GetDepartmentQueryHandler_Handler_Method_Should_Returns_DepartmentDto()
        {
            // Arrange.
            var testDepartment = _testDepartmentList.Cast<Department>().First();
            _request.Id = testDepartment.Id;
            MockDepartmentRepo.Setup(r => r.Read(_request.Id)).Returns(testDepartment);

            // Act.
            var result = await _handler.Handle(_request, None);

            // Assert.
            Assert.AreEqual(typeof(DepartmentDto), result.GetType());
        }

        [Test]
        public async Task GetDepartmentQueryHandler_Handler_Method_Should_Returns_Exception()
        {
            // Arrange.
            MockDepartmentRepo.Setup(r => r.Read(_request.Id)).Returns(null as Department);

            // Act.
            async Task Exception() => await _handler.Handle(_request, None);

            // Assert.
            await Task.FromResult(Assert.ThrowsAsync<NotFoundException>(Exception));
        }
    }
}