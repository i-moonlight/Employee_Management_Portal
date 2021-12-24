using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;
using WebAPI.Tests.Common;
using WebAPI.UserCases.Common.Mappings;
using WebAPI.UserCases.Requests.Employees.Commands.CreateEmployee;
using static System.Threading.CancellationToken;

namespace WebAPI.Tests.Requests.Commands
{
    [TestFixture]
    public class EmployeeCommandHandlersTests
    {
        private Mock<ICrudRepository<Employee>> _mockEmployeeRepo;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var mappingConfig = new MapperConfiguration(c => c.AddProfile(new AssemblyMappingProfile()));
            var mapper = mappingConfig.CreateMapper();

            _mockEmployeeRepo = new Mock<ICrudRepository<Employee>>();
            _mapper = mapper;
        }

        [Test]
        public async Task CreateEmployeeCommandHandler_Handle_Method_Should_Returns_Success_String()
        {
            // Arrange.
            var handler = new CreateEmployeeCommandHandler(_mockEmployeeRepo.Object, _mapper);
            var dto = TestContent.GetTestEmployeeDto();
            var request = new CreateEmployeeCommand() {EmployeeDto = dto};

            // Act.
            var result = await handler.Handle(request, None);

            // Assert.
            Assert.AreEqual("Created successfully", result);
        }

        [Test]
        public async Task CreateEmployeeCommandHandler_Handle_Method_Should_Returns_Failure_String()
        {
            // Arrange.
            var handler = new CreateEmployeeCommandHandler(_mockEmployeeRepo.Object, _mapper);

            // Act.
            var result = await handler.Handle(null, None);

            // Assert.
            Assert.AreEqual("Create failed", result);
        }
    }
}