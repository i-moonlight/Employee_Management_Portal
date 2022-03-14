using System.Threading.Tasks;
using NUnit.Framework;
using WebAPI.Tests.Common;
using WebAPI.UseCases.Requests.Departments.Commands;
using static WebAPI.Utils.Constants.MessageTypes;
using static System.Threading.CancellationToken;

namespace WebAPI.Tests.Requests.Commands
{
    [TestFixture]
    public class DepartmentCommandHandlersTests : RequestTestSetup
    {
        [Test]
        public async Task CreateDepartmentCommandHandler_Handle_Method_Should_Returns_Success_String()
        {
            // Arrange.
            var handler = new CreateDepartmentCommandHandler(MockDepartmentRepo.Object, Mapper);
            var request = new CreateDepartmentCommand() { DepartmentDto = FakeDepartmentDto };

            // Act.
            var result = await handler.Handle(request, None);

            // Assert.
            Assert.AreEqual(CreatedSuccessfull, result);
        }

        [Test]
        public async Task CreateDepartmentCommandHandler_Handle_Method_Should_Returns_Failure_String()
        {
            // Arrange.
            var handler = new CreateDepartmentCommandHandler(MockDepartmentRepo.Object, Mapper);

            // Act.
            var result = await handler.Handle(null, None);

            // Assert.
            Assert.AreEqual(CreatedFailed, result);
        }

        [Test]
        public async Task UpdateDepartmentCommandHandler_Handle_Method_Should_Returns_Success_String()
        {
            // Arrange.
            var handler = new UpdateDepartmentCommandHandler(MockDepartmentRepo.Object, Mapper);
            var request = new UpdateDepartmentCommand() { DepartmentDto = FakeDepartmentDto };

            // Act.
            var result = await handler.Handle(request, None);

            // Assert.
            Assert.AreEqual(UpdatedSuccessfull, result);
        }

        [Test]
        public async Task UpdateDepartmentCommandHandler_Handle_Method_Should_Returns_Failure_String()
        {
            // Arrange.
            var handler = new UpdateDepartmentCommandHandler(MockDepartmentRepo.Object, Mapper);

            // Act.
            var result = await handler.Handle(null, None);

            // Assert.
            Assert.AreEqual(UpdatedFailed, result);
        }

        [Test]
        public async Task DeleteDepartmentCommandHandler_Handle_Method_Should_Returns_Success_String()
        {
            // Arrange.
            var handler = new DeleteDepartmentCommandHandler(MockDepartmentRepo.Object);
            var departmentId = FakeDepartmentDto.Id;
            var request = new DeleteDepartmentCommand() { Id = departmentId };

            // Act.
            var result = await handler.Handle(request, None);

            // Assert.
            Assert.AreEqual(DeletedSuccessfull, result);
        }

        [Test]
        public async Task DeleteDepartmentCommandHandler_Handle_Method_Should_Returns_Failure_String()
        {
            // Arrange.
            var handler = new DeleteDepartmentCommandHandler(MockDepartmentRepo.Object);

            // Act.
            var result = await handler.Handle(null, None);

            // Assert.
            Assert.AreEqual(DeletedFailed, result);
        }
    }
}