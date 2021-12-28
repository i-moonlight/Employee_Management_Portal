using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Moq;
using NUnit.Framework;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;
using WebAPI.Tests.Common;
using WebAPI.UserCases.Common.Mappings;
using WebAPI.UserCases.Requests.Employees.Commands.CreateEmployee;
using WebAPI.UserCases.Requests.Employees.Commands.UpdateEmployeePhoto;
using WebAPI.UserCases.Requests.Employees.Commands.UploadEmployeePhoto;
using WebAPI.UserCases.Requests.Employees.Commands.UpdateEmployee;
using WebAPI.UserCases.Requests.Employees.Commands.DeleteEmployee;
using static System.Threading.CancellationToken;

namespace WebAPI.Tests.Requests.Commands
{
    [TestFixture]
    public class EmployeeCommandHandlersTests
    {
        private Mock<ICrudRepository<Employee>> _mockEmployeeRepo;
        private Mock<IWebHostEnvironment> _mockEnvironment;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var mappingConfig = new MapperConfiguration(c => c.AddProfile(new AssemblyMappingProfile()));
            var mapper = mappingConfig.CreateMapper();

            _mockEmployeeRepo = new Mock<ICrudRepository<Employee>>();
            _mockEnvironment = new Mock<IWebHostEnvironment>();
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

        [Test]
        public async Task UploadPhotoCommandHandler_Handle_Method_Should_Returns_Default_FileName_String()
        {
            // Arrange.
            var handler = new UploadPhotoCommandHandler(_mockEnvironment.Object);

            // Act.
            var result = await handler.Handle(new UploadPhotoCommand(), None);

            // Assert.
            Assert.AreEqual("anonymous.png", result);
        }

        [Test]
        public async Task UpdatePhotoCommandHandler_Handle_Method_Should_Returns_Default_FileName_String()
        {
            // Arrange.
            var handler = new UpdatePhotoCommandHandler(_mockEmployeeRepo.Object, _mockEnvironment.Object);

            // Act.
            var result = await handler.Handle(new UpdatePhotoCommand(), None);

            // Assert.
            Assert.AreEqual("anonymous.png", result);
        }

        [Test]
        public async Task UpdateEmployeeCommandHandler_Handle_Method_Should_Returns_Success_String()
        {
            // Arrange.
            var handler = new UpdateEmployeeCommandHandler(_mockEmployeeRepo.Object, _mapper);
            var dto = TestContent.GetTestEmployeeDto();
            var request = new UpdateEmployeeCommand() {EmployeeDto = dto};

            // Act.
            var result = await handler.Handle(request, None);

            // Assert.
            Assert.AreEqual("Updated successfully", result);
        }

        [Test]
        public async Task UpdateEmployeeCommandHandler_Handle_Method_Should_Returns_Failure_String()
        {
            // Arrange.
            var handler = new UpdateEmployeeCommandHandler(_mockEmployeeRepo.Object, _mapper);

            // Act.
            var result = await handler.Handle(null, None);

            // Assert.
            Assert.AreEqual("Update failed", result);
        }

        [Test]
        public async Task DeleteEmployeeCommandHandler_Handle_Method_Should_Returns_Success_String()
        {
            // Arrange.
            var handler = new DeleteEmployeeCommandHandler(_mockEmployeeRepo.Object);
            var employeeId = TestContent.GetTestEmployeeDto().Id;
            var request = new DeleteEmployeeCommand() {Id = employeeId};

            // Act.
            var result = await handler.Handle(request, None);

            // Assert.
            Assert.AreEqual("Deleted successfully", result);
        }

        [Test]
        public async Task DeleteEmployeeCommandHandler_Handle_Method_Should_Returns_Failure_String()
        {
            // Arrange.
            var handler = new DeleteEmployeeCommandHandler(_mockEmployeeRepo.Object);

            // Act.
            var result = await handler.Handle(null, None);

            // Assert.
            Assert.AreEqual("Delete failed", result);
        }
    }
}