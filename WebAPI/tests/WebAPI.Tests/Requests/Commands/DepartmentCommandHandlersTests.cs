using System.Threading.Tasks;
using NUnit.Framework;
using WebAPI.Tests.Common;
using WebAPI.UserCases.Requests.Departments.Commands.CreateDepartment;
using WebAPI.UserCases.Requests.Departments.Commands.DeleteDepartment;
using WebAPI.UserCases.Requests.Departments.Commands.UpdateDepartment;
using static System.Threading.CancellationToken;

namespace WebAPI.Tests.Requests.Commands
{
    [TestFixture]
    public class DepartmentCommandHandlersTests : RequestHandlersTest
    {
        [Test]
        public async Task CreateDepartmentCommandHandler_Handle_Method_Should_Returns_Success_String()
        {
            // Arrange.
            var handler = new CreateDepartmentCommandHandler(MockDepartmentRepo.Object, Mapper);
            var request = new CreateDepartmentCommand() {DepartmentDto = TestDepartmentDto};

            // Act.
            var result = await handler.Handle(request, None);

            // Assert.
            Assert.AreEqual("Created successfully", result);
        }

        [Test]
        public async Task CreateDepartmentCommandHandler_Handle_Method_Should_Returns_Failure_String()
        {
            // Arrange.
            var handler = new CreateDepartmentCommandHandler(MockDepartmentRepo.Object, Mapper);

            // Act.
            var result = await handler.Handle(null, None);

            // Assert.
            Assert.AreEqual("Create failed", result);
        }

        [Test]
        public async Task UpdateDepartmentCommandHandler_Handle_Method_Should_Returns_Success_String()
        {
            // Arrange.
            var handler = new UpdateDepartmentCommandHandler(MockDepartmentRepo.Object, Mapper);
            var request = new UpdateDepartmentCommand() {DepartmentDto = TestDepartmentDto};

            // Act.
            var result = await handler.Handle(request, None);

            // Assert.
            Assert.AreEqual("Updated successfully", result);
        }

        [Test]
        public async Task UpdateDepartmentCommandHandler_Handle_Method_Should_Returns_Failure_String()
        {
            // Arrange.
            var handler = new UpdateDepartmentCommandHandler(MockDepartmentRepo.Object, Mapper);

            // Act.
            var result = await handler.Handle(null, None);

            // Assert.
            Assert.AreEqual("Update failed", result);
        }

        [Test]
        public async Task DeleteDepartmentCommandHandler_Handle_Method_Should_Returns_Success_String()
        {
            // Arrange.
            var handler = new DeleteDepartmentCommandHandler(MockDepartmentRepo.Object);
            var departmentId = TestDepartmentDto.Id;
            var request = new DeleteDepartmentCommand() {Id = departmentId};

            // Act.
            var result = await handler.Handle(request, None);

            // Assert.
            Assert.AreEqual("Deleted successfully", result);
        }

        [Test]
        public async Task DeleteDepartmentCommandHandler_Handle_Method_Should_Returns_Failure_String()
        {
            // Arrange.
            var handler = new DeleteDepartmentCommandHandler(MockDepartmentRepo.Object);

            // Act.
            var result = await handler.Handle(null, None);

            // Assert.
            Assert.AreEqual("Delete failed", result);
        }
    }
}