using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BotickAPI.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnectionString") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<BotickDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddScoped<IBotickDbContext, BotickDbContext>();
            return services;
        }
    }
}