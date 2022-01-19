using System.Threading.Tasks;
using NUnit.Framework;
using WebAPI.Tests.Common;
using WebAPI.UserCases.Requests.Employees.Commands.CreateEmployee;
using WebAPI.UserCases.Requests.Employees.Commands.UpdateEmployeePhoto;
using WebAPI.UserCases.Requests.Employees.Commands.UploadEmployeePhoto;
using WebAPI.UserCases.Requests.Employees.Commands.UpdateEmployee;
using WebAPI.UserCases.Requests.Employees.Commands.DeleteEmployee;
using static System.Threading.CancellationToken;

namespace WebAPI.Tests.Requests.Commands
{
    [TestFixture]
    public class EmployeeCommandHandlersTests : RequestHandlersTest
    {
        [Test]
        public async Task CreateEmployeeCommandHandler_Handle_Method_Should_Returns_Success_String()
        {
            // Arrange.
            var handler = new CreateEmployeeCommandHandler(MockEmployeeRepo.Object, Mapper);
            var request = new CreateEmployeeCommand() {EmployeeDto = TestEmployeeDto};

            // Act.
            var result = await handler.Handle(request, None);

            // Assert.
            Assert.AreEqual("Created successfully", result);
        }

        [Test]
        public async Task CreateEmployeeCommandHandler_Handle_Method_Should_Returns_Failure_String()
        {
            // Arrange.
            var handler = new CreateEmployeeCommandHandler(MockEmployeeRepo.Object, Mapper);

            // Act.
            var result = await handler.Handle(null, None);

            // Assert.
            Assert.AreEqual("Create failed", result);
        }

        [Test]
        public async Task UploadPhotoCommandHandler_Handle_Method_Should_Returns_Default_FileName_String()
        {
            // Arrange.
            var handler = new UploadPhotoCommandHandler(MockEnvironment.Object);

            // Act.
            var result = await handler.Handle(new UploadPhotoCommand(), None);

            // Assert.
            Assert.AreEqual("anonymous.png", result);
        }

        [Test]
        public async Task UpdatePhotoCommandHandler_Handle_Method_Should_Returns_Default_FileName_String()
        {
            // Arrange.
            var handler = new UpdatePhotoCommandHandler(MockEmployeeRepo.Object, MockEnvironment.Object);

            // Act.
            var result = await handler.Handle(new UpdatePhotoCommand(), None);

            // Assert.
            Assert.AreEqual("anonymous.png", result);
        }

        [Test]
        public async Task UpdateEmployeeCommandHandler_Handle_Method_Should_Returns_Success_String()
        {
            // Arrange.
            var handler = new UpdateEmployeeCommandHandler(MockEmployeeRepo.Object, Mapper);
            var request = new UpdateEmployeeCommand() {EmployeeDto = TestEmployeeDto};

            // Act.
            var result = await handler.Handle(request, None);

            // Assert.
            Assert.AreEqual("Updated successfully", result);
        }

        [Test]
        public async Task UpdateEmployeeCommandHandler_Handle_Method_Should_Returns_Failure_String()
        {
            // Arrange.
            var handler = new UpdateEmployeeCommandHandler(MockEmployeeRepo.Object, Mapper);

            // Act.
            var result = await handler.Handle(null, None);

            // Assert.
            Assert.AreEqual("Update failed", result);
        }

        [Test]
        public async Task DeleteEmployeeCommandHandler_Handle_Method_Should_Returns_Success_String()
        {
            // Arrange.
            var handler = new DeleteEmployeeCommandHandler(MockEmployeeRepo.Object);
            var employeeId = TestEmployeeDto.Id;
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
            var handler = new DeleteEmployeeCommandHandler(MockEmployeeRepo.Object);

            // Act.
            var result = await handler.Handle(null, None);

            // Assert.
            Assert.AreEqual("Delete failed", result);
        }
    }
}