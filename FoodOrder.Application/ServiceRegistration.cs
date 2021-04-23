using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using FoodOrder.Application.Interfaces;
using FoodOrder.Application.Infrastructure.Security;
using FoodOrder.Application.Infrastructure.Users;

namespace FoodOrder.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationRegistration(this IServiceCollection services)
        {
            var assm = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(assm);
            services.AddMediatR(assm);

            services.AddScoped<IJWTGenerator, JWTGenerator>();
            services.AddScoped<IUserAccessor,UserAccessor>();
        }
    }
}