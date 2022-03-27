using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using WebAPI.Service.Authentication.UseCases.Services;

namespace WebAPI.Service.Authentication.UseCases.Implementations
{
    public class HttpService : IHttpService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public HttpService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetUserId()
        {
            return _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public bool IsAuthenticated()
        {
            return _contextAccessor.HttpContext!.User.Identity!.IsAuthenticated;
        }

        /// <summary>  
        /// Check the cookie. 
        /// </summary>  
        /// <param name="key">Key.</param>  
        /// <returns>Keys collection.</returns>  
        public ICollection<string> CheckCookies()
        {
            return _contextAccessor.HttpContext!.Request.Cookies.Keys;
        }

        /// <summary>  
        /// Get the cookie.
        /// </summary>  
        /// <param name="key">Key.</param>  
        /// <returns>string value.</returns>  
        public string GetCookies(string key)
        {
            return _contextAccessor.HttpContext?.Request.Cookies[key];
        }

        /// <summary>  
        /// Set the cookie  
        /// </summary>  
        /// <param name="key">Key (unique indentifier).</param>  
        /// <param name="value">Value to store in cookie object.</param>  
        /// <param name="expireTime">Expiration time.</param>  
        public void SetCookies(string key, string value, int? expireTime)
        {
            var option = new CookieOptions();

            if (expireTime.HasValue)
            {
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            }
            else
            {
                option.Expires = DateTime.Now.AddMilliseconds(10);
            }

            _contextAccessor.HttpContext.Response.Cookies.Append(key, value, option);
        }

        /// <summary>  
        /// Delete the key.  
        /// </summary>  
        /// <param name="key">Key.</param>  
        public void RemoveCookies(string key)
        {
            _contextAccessor.HttpContext.Response.Cookies.Delete(key);
        }
    }
}