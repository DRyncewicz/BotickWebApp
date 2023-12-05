﻿using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BotickAPI.Infrastructure.FileServices;

namespace BotickAPI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var fileSaveConfigSection = configuration.GetSection("FileSaveConfig");
            services.Configure<FileSaveConfig>(options =>
            {
                fileSaveConfigSection.Bind(options);
            });

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IFileSaver, FileSaver>();
            return services;
        }
    }
}
