using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;
using WebAPI.Tests.Common;
using WebAPI.UserCases.Common.Dto;
using WebAPI.UserCases.Common.Mappings;
using WebAPI.UserCases.Requests.Employees.Queries.GetEmployee;

namespace WebAPI.Tests.Handlers.Queries
{
    public class GetEmployeeQueryHandlerTest
    {
        private Mock<ICrudRepository<Employee>> _mockEmployeeRepo;
        private IMapper _mapper;
        private GetEmployeeQuery _request;

        [SetUp]
        public void Setup()
        {
            _request = new GetEmployeeQuery();
            _mockEmployeeRepo = new Mock<ICrudRepository<Employee>>();

            var mappingConfig = new MapperConfiguration(c => c.AddProfile(new AssemblyMappingProfile()));
            var mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }

        [Test]
        public async Task GetEmployeeQueryHandler_Handler_Method_Should_Returns_EmployeeDto()
        {
            // Arrange.
            var handler = new GetEmployeeQueryHandler(_mockEmployeeRepo.Object, _mapper);
            var employee = TestContent.GetTestEmployeeList().Cast<Employee>().ToList().First();
            _request.Id = employee.Id;
            _mockEmployeeRepo.Setup(r => r.Read(_request.Id)).Returns(employee);

            // Act.
            var result = await handler.Handle(_request, CancellationToken.None);

            // Assert.
            Assert.AreEqual(typeof(EmployeeDto), result.GetType());
        }
    }
}