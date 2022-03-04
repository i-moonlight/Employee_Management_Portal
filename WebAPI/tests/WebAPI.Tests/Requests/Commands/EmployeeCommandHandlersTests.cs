using System.Threading.Tasks;
using NUnit.Framework;
using WebAPI.Tests.Common;
using WebAPI.UseCases.Requests.Employees.Commands;
using static System.Threading.CancellationToken;
using static WebAPI.Utils.Constants.MessageTypes;

namespace WebAPI.Tests.Requests.Commands
{
    [TestFixture]
    public class EmployeeCommandHandlersTests : RequestTestSetup
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
            Assert.AreEqual(CreatedSuccessfull, result);
        }

        [Test]
        public async Task CreateEmployeeCommandHandler_Handle_Method_Should_Returns_Failure_String()
        {
            // Arrange.
            var handler = new CreateEmployeeCommandHandler(MockEmployeeRepo.Object, Mapper);

            // Act.
            var result = await handler.Handle(null, None);

            // Assert.
            Assert.AreEqual(CreatedFailed, result);
        }

        [Test]
        public async Task UploadPhotoCommandHandler_Handle_Method_Should_Returns_Default_FileName_String()
        {
            // Arrange.
            var handler = new UploadPhotoCommandHandler(MockEnvironment.Object);

            // Act.
            var result = await handler.Handle(new UploadPhotoCommand(), None);

            // Assert.
            Assert.AreEqual(NamePhotoDefault, result);
        }

        [Test]
        public async Task UpdatePhotoCommandHandler_Handle_Method_Should_Returns_Default_FileName_String()
        {
            // Arrange.
            var handler = new UpdatePhotoCommandHandler(MockEmployeeRepo.Object, MockEnvironment.Object);

            // Act.
            var result = await handler.Handle(new UpdatePhotoCommand(), None);

            // Assert.
            Assert.AreEqual(NamePhotoDefault, result);
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
            Assert.AreEqual(UpdatedSuccessfull, result);
        }

        [Test]
        public async Task UpdateEmployeeCommandHandler_Handle_Method_Should_Returns_Failure_String()
        {
            // Arrange.
            var handler = new UpdateEmployeeCommandHandler(MockEmployeeRepo.Object, Mapper);

            // Act.
            var result = await handler.Handle(null, None);

            // Assert.
            Assert.AreEqual(UpdatedFailed, result);
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
            Assert.AreEqual(DeletedSuccessfull, result);
        }

        [Test]
        public async Task DeleteEmployeeCommandHandler_Handle_Method_Should_Returns_Failure_String()
        {
            // Arrange.
            var handler = new DeleteEmployeeCommandHandler(MockEmployeeRepo.Object);

            // Act.
            var result = await handler.Handle(null, None);

            // Assert.
            Assert.AreEqual(DeletedFailed, result);
        }
    }
}