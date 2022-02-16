using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using WebAPI.DataAccess.MsSql.Persistence.Context;
using WebAPI.UserCases.Common.Dto;
using WebAPI.UserCases.Common.Dto.Request;
using WebAPI.Web;

namespace WebAPI.Tests.Common
{
    public class ControllerTestSetup
    {
        protected TestWebApplicationFactory<Startup> WebApplicationFactory;
        protected DepartmentDto TestDepartmentDto;
        protected EmployeeDto TestEmployeeDto;
        protected LoginDto TestLoginDto;
        protected RegisterUserDto TestRegisterUserDto;
        protected HttpClient HttpClient;
        protected AppDbContext TestDbContext;

        [SetUp]
        public void Setup()
        {
            WebApplicationFactory = new TestWebApplicationFactory<Startup>();
            TestDepartmentDto = TestContent.TestDepartmentDto;
            TestEmployeeDto = TestContent.TestEmployeeDto;
            TestLoginDto = TestContent.TestLoginDto;
            TestRegisterUserDto = TestContent.TestRegisterUserDto;
            HttpClient = WebApplicationFactory.CreateClient();
            TestDbContext = WebApplicationFactory.Services.CreateScope().ServiceProvider.GetService<AppDbContext>();
        }
    }
}