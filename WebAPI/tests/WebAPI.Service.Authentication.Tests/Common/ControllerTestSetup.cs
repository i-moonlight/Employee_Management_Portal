using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using WebAPI.Service.Authentication.Database;
using WebAPI.Service.Authentication.UseCases.Dto;

namespace WebAPI.Service.Authentication.Tests.Common
{
    public class ControllerTestSetup
    {
        protected TestWebApplicationFactory<Startup> WebApplicationFactory;
        protected HttpClient HttpClient;
        protected AuthDbContext FakeDbContext;
        protected RegisterUserDto FakeRegisterUserDto;
        protected LoginDto FakeLoginDto;

        [SetUp]
        public void Setup()
        {
            FakeRegisterUserDto = FakeAuthContent.FakeRegisterUserDto;
            FakeLoginDto = FakeAuthContent.FakeLoginDto;
            WebApplicationFactory = new TestWebApplicationFactory<Startup>();
            HttpClient = WebApplicationFactory.CreateClient();
            FakeDbContext = WebApplicationFactory.Services.CreateScope().ServiceProvider.GetService<AuthDbContext>();
        }
    }
}