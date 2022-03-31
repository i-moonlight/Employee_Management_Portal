using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using WebAPI.Service.Authentication.UseCases.Dto;

namespace WebAPI.Service.Authentication.Tests.Common
{
    public static class FakeAuthContent
    {
        public static LoginDto FakeLoginDto => new()
        {
            Username = "UserName",
            Email = "User@test.ru",
            Password = "User123!"
        };
        
        public static RegisterUserDto FakeRegisterUserDto => new()
        {
            FullName = "FullName",
            UserName = "UserName",
            Email = "User@test.ru",
            Password = "User123!"
        };
        
        public static AccountDto FakeAccountDto => new()
        {
            Email = "User@test.ru"
        };
        
        public static StringContent GetRequestContent(object obj) =>
            new(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
    }
}