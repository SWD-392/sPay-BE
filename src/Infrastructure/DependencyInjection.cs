using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrashtructure(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("LocalConnection");
            services.AddDbContext<ExampleDbContext>(options => options.UseSqlServer(connection));
            return services;
        }
    }
}
