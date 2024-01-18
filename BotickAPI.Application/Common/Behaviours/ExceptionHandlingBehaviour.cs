using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotickAPI.Application.Common.Exceptions;
using TimeoutException = BotickAPI.Application.Common.Exceptions.TimeoutException;

namespace BotickAPI.Application.Common.Behaviours
{
    public class ExceptionHandlerBehaviour<TRequest, TResponse>(ILogger<ExceptionHandlerBehaviour<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (NotFoundException ex)
            {
                var requestName = typeof(TRequest).Name;
                logger.LogError(ex,
                    "Botick Not Found Exception: record was not found for Request {Name} {@Request}", requestName,
                    request);
                throw;
            }
            catch (ValidationException ex)
            {
                var requestName = typeof(TRequest).Name;
                logger.LogError(ex,
                    "Botick Validation Exception: bad value/s for Request {Name} {@Request}", requestName,
                    request);
                throw;
            }
            catch (BadRequestException ex)
            {
                var requestName = typeof(TRequest).Name;
                logger.LogError(ex,
                    "Botick Bad Request Exception: bad  Request {Name} {@Request}", requestName,
                    request);
                throw;
            }
            catch (ConflictException ex)
            {
                var requestName = typeof(TRequest).Name;
                logger.LogError(ex,
                    "Botick Conflict Exception: Conflict for request {Name} {@Request}", requestName,
                    request);
                throw;
            }
            catch (TimeoutException ex)
            {
                var requestName = typeof(TRequest).Name;
                logger.LogError(ex,
                    "Botick Timeout Exception: Timeout for request {Name} {@Request}", requestName,
                    request);
                throw;
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                logger.LogError(ex,
                    "Botick Exception: Unhandled Exception for Request {Name} {@Request}", requestName,
                    request);
                throw;
            }
        }
    }
}