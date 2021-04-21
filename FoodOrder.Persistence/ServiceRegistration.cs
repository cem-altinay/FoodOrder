using System;
using System.Reflection;
using FoodOrder.Application.Interfaces.Repositories;
using FoodOrder.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOrder.Persistence
{
    public static class ServiceRegistration
    {

        public static void AddPersistenceRegistration(this IServiceCollection services)
        {

          services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        }
    }
}