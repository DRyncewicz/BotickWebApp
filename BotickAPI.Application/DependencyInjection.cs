using BotickAPI.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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

                return services;
            }
        }
    }
}
