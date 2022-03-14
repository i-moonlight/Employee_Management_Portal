using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using WebAPI.Entities.Models;
using WebAPI.Tests.Common;
using WebAPI.UseCases.Common.Dto;
using WebAPI.UseCases.Common.Exceptions;
using WebAPI.UseCases.Requests.Departments.Queries;
using static System.Threading.CancellationToken;

namespace WebAPI.Tests.Requests.Queries
{
    [TestFixture]
    public class DepartmentQueryHandlersTests : RequestTestSetup
    {
        private GetDepartmentQuery _request;
        private GetDepartmentQueryHandler _handler;
        private IEnumerable _fakeDepartmentList;

        [SetUp]
        public new void Setup()
        {
            _request = new GetDepartmentQuery();
            _handler = new GetDepartmentQueryHandler(MockDepartmentRepo.Object, Mapper);
            _fakeDepartmentList = FakeTestContent.FakeDepartmentList;
        }

        [Test]
        public async Task GetDepartmentListQueryHandler_Handler_Method_Should_Returns_Department_List()
        {
            // Arrange.
            var handler = new GetDepartmentListQueryHandler(MockDepartmentRepo.Object);
            MockDepartmentRepo.Setup(r => r.Read()).Returns(_fakeDepartmentList);

            // Act.
            var result = await handler.Handle(new GetDepartmentListQuery(), None);

            // Assert.
            Assert.AreEqual(typeof(List<Department>), result.GetType());
        }

        [Test]
        public async Task GetDepartmentQueryHandler_Handler_Method_Should_Returns_DepartmentDto()
        {
            // Arrange.
            var fakeDepartment = _fakeDepartmentList.Cast<Department>().First();
            _request.Id = fakeDepartment.Id;
            MockDepartmentRepo.Setup(r => r.Read(_request.Id)).Returns(fakeDepartment);

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