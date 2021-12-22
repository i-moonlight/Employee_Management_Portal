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
using WebAPI.UserCases.Requests.Employees.Queries.GetDepartmentNameList;
using WebAPI.UserCases.Requests.Employees.Queries.GetEmployee;
using WebAPI.UserCases.Requests.Employees.Queries.GetEmployeeList;
using static System.Threading.CancellationToken;

namespace WebAPI.Tests.Requests.Queries
{
    [TestFixture]
    public class EmployeeQueryHandlersTests
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
        public async Task GetEmployeeListQueryHandler_Handler_Method_Should_Returns_Employee_List()
        {
            // Arrange.
            var handler = new GetEmployeeListQueryHandler(_mockEmployeeRepo.Object);
            var employeeList = TestContent.GetTestEmployeeList();

            _mockEmployeeRepo.Setup(r => r.Read()).Returns(employeeList);

            // Act.
            var result = await handler.Handle(new GetEmployeeListQuery(), None);

            // Assert.
            Assert.AreEqual(typeof(List<Employee>), result.GetType());
        }

        [Test]
        public async Task GetEmployeeQueryHandler_Handler_Method_Should_Returns_EmployeeDto()
        {
            // Arrange.
            var request = new GetEmployeeQuery();
            var handler = new GetEmployeeQueryHandler(_mockEmployeeRepo.Object, _mapper);
            var employee = TestContent.GetTestEmployeeList().Cast<Employee>().ToList().First();

            request.Id = employee.Id;
            _mockEmployeeRepo.Setup(r => r.Read(request.Id)).Returns(employee);

            // Act.
            var result = await handler.Handle(request, None);

            // Assert.
            Assert.AreEqual(typeof(EmployeeDto), result.GetType());
        }

        [Test]
        public async Task GetDepartmentNameListQueryHandler_Handler_Method_Should_Returns_Name_List()
        {
            // Arrange.
            var handler = new GetDepartmentNameListQueryHandler(_mockEmployeeRepo.Object);
            var departmentName = TestContent.GetTestDepartmentList()
                .Cast<Department>()
                .OrderBy(d => d.Name).Select(d => d.Name)
                .ToList();

            _mockEmployeeRepo.Setup(r => r.ReadAll()).Returns(departmentName);

            // Act.
            var result = await handler.Handle(new GetDepartmentNameListQuery(), None);

            // Assert.
            Assert.AreEqual(typeof(List<string>), result.GetType());
        }
    }
}