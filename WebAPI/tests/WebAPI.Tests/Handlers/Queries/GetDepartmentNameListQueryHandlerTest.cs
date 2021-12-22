using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;
using WebAPI.Tests.Common;
using WebAPI.UserCases.Requests.Employees.Queries.GetDepartmentNameList;

namespace WebAPI.Tests.Handlers.Queries
{
    [TestFixture]
    public class GetDepartmentNameListQueryHandlerTest
    {
        private Mock<ICrudRepository<Employee>> _mockEmployeeRepo;

        [SetUp]
        public void Setup()
        {
            _mockEmployeeRepo = new Mock<ICrudRepository<Employee>>();
        }

        [Test]
        public async Task GetDepartmentNameListQueryHandler_Handler_Method_Should_Returns_Name_List()
        {
            // Arrange.
            var handler = new GetDepartmentNameListQueryHandler(_mockEmployeeRepo.Object);

            var departmentName = TestContent.GetTestDepartmentList()
                .Cast<Department>()
                .OrderBy(d => d.Name).Select(d => d.Name)
                .ToList();

            _mockEmployeeRepo.Setup(r => r.ReadAll()).Returns(departmentName);

            // Act.
            var result = await handler.Handle(new GetDepartmentNameListQuery(), CancellationToken.None);

            // Assert.
            Assert.AreEqual(typeof(List<string>), result.GetType(), "Return type mismatch");
        }
    }
}