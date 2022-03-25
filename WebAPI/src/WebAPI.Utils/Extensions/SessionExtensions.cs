using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Utils.Extensions
{
    public static class SessionExtensions
    {
        // Individual key for storing session user data
        public const string UserKey = "UserKey";
        
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
        }
    }
}