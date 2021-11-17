using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace WebAPI.UserCases.Common.Mappings
{
    /// <summary>
    /// Assembly mapping profile.
    /// </summary>
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile(Assembly getExecutingAssembly) => 
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

        /// <summary>
        /// Apply mappings from assembly.
        /// </summary>
        /// <param name="assembly">The assembly</param>
        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces()
                    .Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IMapWith<>))).ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping") ?? type.GetInterface("IMapFrom`1")!.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] {this});
            }
        }
    }
}