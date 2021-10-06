using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using WebAPI.DataAccess.Persistence;
using WebAPI.UseCases.Dto;

namespace WebAPI.Tests.Setup
{
    public abstract class ControllerTestSetup
    {
        protected AppDbContext FakeDbContext;
        protected EmployeeDto FakeEmployeeDto;
        protected HttpClient HttpClient;
        protected TestApplicationFactory<Startup> WebHost;

        [SetUp]
        public void Setup()
        {
            FakeEmployeeDto = FakeTestContent.FakeEmployeeDto;
            WebHost = new TestApplicationFactory<Startup>();
            HttpClient = WebHost.CreateClient();
            
            FakeDbContext = WebHost
                .Services.CreateScope()
                .ServiceProvider.GetService<AppDbContext>();
        }
    }
}
