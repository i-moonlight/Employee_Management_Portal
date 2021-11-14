﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace WebAPI.UserCases
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}