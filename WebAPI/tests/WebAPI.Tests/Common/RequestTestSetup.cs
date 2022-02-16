using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Moq;
using NUnit.Framework;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;
using WebAPI.UserCases.Common.Dto;
using WebAPI.UserCases.Common.Mappings;

namespace WebAPI.Tests.Common
{
    public class RequestTestSetup
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
            var mapperConfig = new MapperConfiguration(e => e.AddProfile(new AssemblyMappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            TestDepartmentDto = TestContent.TestDepartmentDto;
            TestEmployeeDto = TestContent.TestEmployeeDto;
            MockDepartmentRepo = new Mock<ICrudRepository<Department>>();
            MockEmployeeRepo = new Mock<ICrudRepository<Employee>>();
            MockEnvironment = new Mock<IWebHostEnvironment>();
            Mapper = mapper;
        }
    }
}