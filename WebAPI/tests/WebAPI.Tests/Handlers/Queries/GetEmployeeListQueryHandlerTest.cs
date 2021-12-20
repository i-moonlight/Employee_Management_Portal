using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;
using WebAPI.Tests.Common;
using WebAPI.UserCases.Requests.Employees.Queries.GetEmployeeList;

namespace WebAPI.Tests.Handlers.Queries
{
    [TestFixture]
    public class GetEmployeeListQueryHandlerTest
    {
        private Mock<ICrudRepository<Employee>> _mockEmployeeRepo;

        [SetUp]
        public void Setup()
        {
            _mockEmployeeRepo = new Mock<ICrudRepository<Employee>>();
        }

        [Test]
        public async Task GetEmployeeListQueryHandler_Handler_Method_Should_Returns_Employee_List()
        {
            // Arrange.
            var handler = new GetEmployeeListQueryHandler(_mockEmployeeRepo.Object);
            var employeeList = TestContent.GetTestEmployeeList();
            _mockEmployeeRepo.Setup(r => r.Read()).Returns(employeeList);

            // Act.
            var result = await handler.Handle(new GetEmployeeListQuery(), CancellationToken.None);

            // Assert.
            Assert.AreEqual(typeof(List<Employee>), result.GetType());
        }
    }
}