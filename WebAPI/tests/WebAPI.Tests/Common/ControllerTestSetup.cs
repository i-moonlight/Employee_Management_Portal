using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using WebAPI.Controllers;
using WebAPI.DataAccess.MsSql.Persistence.Context;
using WebAPI.UserCases.Common.Dto;
using WebAPI.UserCases.Common.Dto.Request;
using WebAPI.Web;
using static WebAPI.Tests.Common.TestContent;

namespace WebAPI.Tests.Common
{
    public class ControllerTestSetup
    {
        protected AuthController AuthController;
        protected DepartmentController DepartmentController;
        protected EmployeeController EmployeeController;
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
            AuthController = new AuthController();
            DepartmentController = new DepartmentController();
            EmployeeController = new EmployeeController();
            WebApplicationFactory = new TestWebApplicationFactory<Startup>();
            TestDepartmentDto = GetTestDepartmentDto();
            TestEmployeeDto = GetTestEmployeeDto();
            TestLoginDto = GetTestLoginDto();
            TestRegisterUserDto = GetTestRegisterUserDto();
            HttpClient = WebApplicationFactory.CreateClient();
            TestDbContext = WebApplicationFactory.Services.CreateScope().ServiceProvider.GetService<AppDbContext>();
        }
    }
}