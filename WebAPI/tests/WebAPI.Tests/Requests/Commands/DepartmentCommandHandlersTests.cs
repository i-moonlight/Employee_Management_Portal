using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;
using WebAPI.Tests.Common;
using WebAPI.UserCases.Common.Mappings;
using WebAPI.UserCases.Requests.Departments.Commands.CreateDepartment;
using WebAPI.UserCases.Requests.Departments.Commands.DeleteDepartment;
using WebAPI.UserCases.Requests.Departments.Commands.UpdateDepartment;
using static System.Threading.CancellationToken;

namespace WebAPI.Tests.Requests.Commands
{
    public class DepartmentCommandHandlersTests
    {
        private Mock<ICrudRepository<Department>> _mockDepartmentRepo;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var mappingConfig = new MapperConfiguration(c => c.AddProfile(new AssemblyMappingProfile()));
            var mapper = mappingConfig.CreateMapper();

            _mockDepartmentRepo = new Mock<ICrudRepository<Department>>();
            _mapper = mapper;
        }

        [Test]
        public async Task CreateDepartmentCommandHandler_Handle_Method_Should_Returns_Success_String()
        {
            // Arrange.
            var handler = new CreateDepartmentCommandHandler(_mockDepartmentRepo.Object, _mapper);
            var dto = TestContent.GetTestDepartmentDto();
            var request = new CreateDepartmentCommand() {DepartmentDto = dto};

            // Act.
            var result = await handler.Handle(request, None);

            // Assert.
            Assert.AreEqual("Created successfully", result);
        }

        [Test]
        public async Task CreateDepartmentCommandHandler_Handle_Method_Should_Returns_Failure_String()
        {
            // Arrange.
            var handler = new CreateDepartmentCommandHandler(_mockDepartmentRepo.Object, _mapper);

            // Act.
            var result = await handler.Handle(null, None);

            // Assert.
            Assert.AreEqual("Create failed", result);
        }

        [Test]
        public async Task UpdateDepartmentCommandHandler_Handle_Method_Should_Returns_Success_String()
        {
            // Arrange.
            var handler = new UpdateDepartmentCommandHandler(_mockDepartmentRepo.Object, _mapper);
            var dto = TestContent.GetTestDepartmentDto();
            var request = new UpdateDepartmentCommand() {DepartmentDto = dto};

            // Act.
            var result = await handler.Handle(request, None);

            // Assert.
            Assert.AreEqual("Updated successfully", result);
        }

        [Test]
        public async Task UpdateDepartmentCommandHandler_Handle_Method_Should_Returns_Failure_String()
        {
            // Arrange.
            var handler = new UpdateDepartmentCommandHandler(_mockDepartmentRepo.Object, _mapper);

            // Act.
            var result = await handler.Handle(null, None);

            // Assert.
            Assert.AreEqual("Update failed", result);
        }
        
        [Test]
        public async Task DeleteDepartmentCommandHandler_Handle_Method_Should_Returns_Success_String()
        {
            // Arrange.
            var handler = new DeleteDepartmentCommandHandler(_mockDepartmentRepo.Object);
            var employeeId = TestContent.GetTestDepartmentDto().Id;
            var request = new DeleteDepartmentCommand() {Id = employeeId};

            // Act.
            var result = await handler.Handle(request, None);

            // Assert.
            Assert.AreEqual("Deleted successfully", result);
        }
    }
}