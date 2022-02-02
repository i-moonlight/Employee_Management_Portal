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
        protected AppDbContext TestDbContext;
        protected DepartmentDto TestDepartmentDto;
        protected EmployeeDto TestEmployeeDto;
        protected LoginDto TestLoginDto;
        protected RegisterUserDto TestRegisterUserDto;
        protected AuthController AuthController;
        protected DepartmentController DepartmentController;
        protected EmployeeController EmployeeController;
        protected HttpClient HttpClient;
        protected TestWebApplicationFactory<Startup> WebApplicationFactory;

        [SetUp]
        public void Setup()
        {
            TestDepartmentDto = GetTestDepartmentDto();
            TestEmployeeDto = GetTestEmployeeDto();
            TestLoginDto = GetTestLoginDto();
            TestRegisterUserDto = GetTestRegisterUserDto();
            AuthController = new AuthController();
            DepartmentController = new DepartmentController();
            EmployeeController = new EmployeeController();
            WebApplicationFactory = new TestWebApplicationFactory<Startup>();
            HttpClient = WebApplicationFactory.CreateClient();

            TestDbContext = WebApplicationFactory
                .Services.CreateScope()
                .ServiceProvider.GetService<AppDbContext>();
        }
    }
}