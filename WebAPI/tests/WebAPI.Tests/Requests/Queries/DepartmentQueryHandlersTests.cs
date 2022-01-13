using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;
using WebAPI.Tests.Common;
using WebAPI.UserCases.Common.Dto;
using WebAPI.UserCases.Common.Mappings;
using WebAPI.UserCases.Requests.Departments.Queries.GetDepartment;
using WebAPI.UserCases.Requests.Departments.Queries.GetDepartmentList;
using static System.Threading.CancellationToken;

namespace WebAPI.Tests.Requests.Queries
{
    public class DepartmentQueryHandlersTests
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
        public async Task GetDepartmentListQueryHandler_Handler_Method_Should_Returns_Department_List()
        {
            // Arrange.
            var handler = new GetDepartmentListQueryHandler(_mockDepartmentRepo.Object);
            var departmentList = TestContent.GetTestDepartmentList();
            _mockDepartmentRepo.Setup(r => r.Read()).Returns(departmentList);

            // Act.
            var result = await handler.Handle(new GetDepartmentListQuery(), None);

            // Assert.
            Assert.AreEqual(typeof(List<Department>), result.GetType());
        }

        [Test]
        public async Task GetDepartmentQueryHandler_Handler_Method_Should_Returns_DepartmentDto()
        {
            // Arrange.
            var request = new GetDepartmentQuery();
            var handler = new GetDepartmentQueryHandler(_mockDepartmentRepo.Object, _mapper);
            var department = TestContent.GetTestDepartmentList().Cast<Department>().ToList().First();

            request.Id = department.Id;
            _mockDepartmentRepo.Setup(r => r.Read(request.Id)).Returns(department);

            // Act.
            var result = await handler.Handle(request, None);

            // Assert.
            Assert.AreEqual(typeof(DepartmentDto), result.GetType());
        }
    }
}