using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;
using WebAPI.Tests.Common;
using WebAPI.UserCases.Requests.Departments.Queries.GetDepartmentList;
using static System.Threading.CancellationToken;

namespace WebAPI.Tests.Requests.Queries
{
    public class DepartmentQueryHandlersTests
    {
        private Mock<ICrudRepository<Department>> _mockDepartmentRepo;

        [SetUp]
        public void Setup()
        {
            _mockDepartmentRepo = new Mock<ICrudRepository<Department>>();
        }

        [Test]
        public async Task GetDepartmentListQueryHandler_Handler_Method_Should_Returns_Department_List()
        {
            // Arrange.
            var handler = new GetDepartmentListQueryHandler(_mockDepartmentRepo.Object);
            var departmentList = TestContent.GetTestDepartmentList();
            _mockDepartmentRepo.Setup(r => r.Read()).Returns(departmentList);

            // Act.
            var result = await handler.Handle(new GetDepartmentListQuery(), None);

            // Assert.
            Assert.AreEqual(typeof(List<Department>), result.GetType());
        }
    }
}