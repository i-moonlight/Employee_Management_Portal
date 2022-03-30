using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using WebAPI.DataAccess.MsSql.Persistence;
using WebAPI.Service.Authentication.UseCases.Dto;
using WebAPI.UseCases.Common.Dto;
using WebAPI.Web;

namespace WebAPI.Tests.Common
{
    public abstract class ControllerTestSetup
    {
        protected TestWebApplicationFactory<Startup> WebApplicationFactory;
        protected DepartmentDto FakeDepartmentDto;
        protected EmployeeDto FakeEmployeeDto;
        protected LoginDto FakeLoginDto;
        protected RegisterUserDto FakeRegisterUserDto;
        protected HttpClient HttpClient;
        protected AppDbContext FakeDbContext;

        [SetUp]
        public void Setup()
        {
            WebApplicationFactory = new TestWebApplicationFactory<Startup>();
            FakeDepartmentDto = FakeTestContent.FakeDepartmentDto;
            FakeEmployeeDto = FakeTestContent.FakeEmployeeDto;
            FakeLoginDto = FakeTestContent.FakeLoginDto;
            FakeRegisterUserDto = FakeTestContent.FakeRegisterUserDto;
            HttpClient = WebApplicationFactory.CreateClient();
            FakeDbContext = WebApplicationFactory.Services.CreateScope().ServiceProvider.GetService<AppDbContext>();
        }
    }
}