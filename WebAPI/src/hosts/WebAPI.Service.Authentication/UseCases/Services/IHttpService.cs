using System.Collections.Generic;

namespace WebAPI.Service.Authentication.UseCases.Services
{
    public interface IHttpService
    {
        string GetUserId();
        bool IsAuthenticated();
        ICollection<string> CheckCookies();
        string GetCookies(string key);
        void SetCookies(string key, string value, int? expireTime);
        void RemoveCookies(string key);
    }
}