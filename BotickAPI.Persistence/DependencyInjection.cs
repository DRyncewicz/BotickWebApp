using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Persistence.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace BotickAPI.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnectionString") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<BotickDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddScoped<IDbConnection>(db => new SqlConnection(configuration.GetConnectionString("DefaultConnectionString")));
            services.AddScoped<IBotickDbContext, BotickDbContext>();
            return services;
        }
    }
}