using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using WebAPI.DataAccess.MsSql.Persistence;
using WebAPI.UseCases.Common.Dto;
using WebAPI.Web;

namespace WebAPI.Tests.Common
{
    public abstract class ControllerTestSetup
    {
        protected DepartmentDto FakeDepartmentDto;
        protected EmployeeDto FakeEmployeeDto;
        protected TestWebApplicationFactory<Startup> WebApplicationFactory;
        protected HttpClient HttpClient;
        protected AppDbContext FakeDbContext;

        [SetUp]
        public void Setup()
        {
            FakeDepartmentDto = FakeTestContent.FakeDepartmentDto;
            FakeEmployeeDto = FakeTestContent.FakeEmployeeDto;
            WebApplicationFactory = new TestWebApplicationFactory<Startup>();
            HttpClient = WebApplicationFactory.CreateClient();
            FakeDbContext = WebApplicationFactory.Services.CreateScope().ServiceProvider.GetService<AppDbContext>();
        }
    }
}