﻿using GeneralCommittee.Application.Common;
using GeneralCommittee.Domain.Constants;
using GeneralCommittee.Domain.Exceptions;

namespace GeneralCommittee.API.MiddleWares
{
    public class GlobalErrorHandling(
    ILogger<GlobalErrorHandling> logger
) : IMiddleware
    {
 public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (AlreadyExist ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (ResourceNotFound ex)
            {
                logger.LogError(ex, "Resource not found: {Message}", ex.Message);
                context.Response.StatusCode = 404;
                var ret = OperationResult<string>.Failure(ex.Message, statusCode: StatusCode.NotFound);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(ret);
            }
            catch (ForBidenException ex)
            {
                logger.LogError(ex, "Forbiden: {Message}", ex.Message);
                context.Response.StatusCode = 401;
                var ret = OperationResult<string>.Failure(ex.Message, statusCode: StatusCode.Forbidden);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(ret);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("An unexpected error occurred.");
            }
        }











    }
}
