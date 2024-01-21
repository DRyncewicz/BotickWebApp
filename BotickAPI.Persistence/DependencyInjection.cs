using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Persistence.Context;
using BotickAPI.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

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
            services.AddScoped(typeof(IBaseCommandRepository<>), typeof(BaseCommandRepository<>));
            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
            services.AddScoped<IDbQueryService, DbQueryService>();

            return services;
        }
    }
}