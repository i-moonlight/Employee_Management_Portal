using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Moq;
using NUnit.Framework;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;
using WebAPI.UserCases.Common.Dto;
using WebAPI.UserCases.Common.Mappings;
using static WebAPI.Tests.Common.TestContent;

namespace WebAPI.Tests.Common
{
    public class RequestHandlersTest
    {
        protected DepartmentDto TestDepartmentDto;
        protected EmployeeDto TestEmployeeDto;
        protected Mock<ICrudRepository<Department>> MockDepartmentRepo;
        protected Mock<ICrudRepository<Employee>> MockEmployeeRepo;
        protected Mock<IWebHostEnvironment> MockEnvironment;
        protected IMapper Mapper;

        [SetUp]
        public void Setup()
        {
            var mappingConfig = new MapperConfiguration(c => c.AddProfile(new AssemblyMappingProfile()));
            var mapper = mappingConfig.CreateMapper();

            TestDepartmentDto = GetTestDepartmentDto();
            TestEmployeeDto = GetTestEmployeeDto();
            MockDepartmentRepo = new Mock<ICrudRepository<Department>>();
            MockEmployeeRepo = new Mock<ICrudRepository<Employee>>();
            MockEnvironment = new Mock<IWebHostEnvironment>();
            Mapper = mapper;
        }
    }
}