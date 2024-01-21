using BotickAPI.Application.Common.Behaviours;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using BotickAPI.Application.Helpers;

namespace BotickAPI.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            {
                services.AddAutoMapper(Assembly.GetExecutingAssembly());
                services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
                services.AddTransient(typeof(IRequestPreProcessor<>), typeof(LoggingBehaviour<>));
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlerBehaviour<,>));
                services.AddTransient<MappingMultiEntityQueryHelper>();

                return services;
            }
        }
    }
}
