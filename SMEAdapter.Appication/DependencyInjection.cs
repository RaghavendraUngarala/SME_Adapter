using Microsoft.Extensions.DependencyInjection;
using SMEAdapter.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration => 
                {
                    configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                });
           
            return services;
        }
    }
}
// This code defines a static class `DependencyInjection` in the `SMEAdapter.Application` namespace.