using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMEAdapter.Domain.Interfaces;
using SMEAdapter.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Infrastructure
{
   public static class DependencyInjection
   {
        public static IServiceCollection AddInfrasturcture(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductDocumentRepository, ProductDocumentRepository>();

            return services;
        }
   }
}
