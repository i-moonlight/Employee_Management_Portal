using AutoMapper;
using Moq;
using NUnit.Framework;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;
using WebAPI.UserCases.Common.Dto;
using WebAPI.UserCases.Common.Mappings;

namespace WebAPI.Tests.Common
{
    public class RequestHandlersTest
    {
        protected DepartmentDto TestDepartmentDto;
        protected Mock<ICrudRepository<Department>> MockDepartmentRepo;
        protected IMapper Mapper;

        [SetUp]
        public void Setup()
        {
            var mappingConfig = new MapperConfiguration(c => c.AddProfile(new AssemblyMappingProfile()));
            var mapper = mappingConfig.CreateMapper();

            TestDepartmentDto = TestContent.GetTestDepartmentDto();
            MockDepartmentRepo = new Mock<ICrudRepository<Department>>();
            Mapper = mapper;
        }
    }
}