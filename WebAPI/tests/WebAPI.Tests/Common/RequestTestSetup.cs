using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Moq;
using NUnit.Framework;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;
using WebAPI.UseCases.Common.Dto;
using WebAPI.UseCases.Common.Mappings;

namespace WebAPI.Tests.Common
{
    public abstract class RequestTestSetup
    {
        protected DepartmentDto FakeDepartmentDto;
        protected EmployeeDto FakeEmployeeDto;
        protected Mock<ICrudRepository<Department>> MockDepartmentRepo;
        protected Mock<ICrudRepository<Employee>> MockEmployeeRepo;
        protected Mock<IWebHostEnvironment> MockEnvironment;
        protected IMapper Mapper;

        [SetUp]
        public void Setup()
        {
            var mapperConfig = new MapperConfiguration(e => e.AddProfile(new AssemblyMappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            FakeDepartmentDto = FakeTestContent.FakeDepartmentDto;
            FakeEmployeeDto = FakeTestContent.FakeEmployeeDto;
            MockDepartmentRepo = new Mock<ICrudRepository<Department>>();
            MockEmployeeRepo = new Mock<ICrudRepository<Employee>>();
            MockEnvironment = new Mock<IWebHostEnvironment>();
            Mapper = mapper;
        }
    }
}