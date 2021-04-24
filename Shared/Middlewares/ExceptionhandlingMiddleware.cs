using System;
using System.Threading.Tasks;
using FoodOrder.Shared.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FoodOrder.Shared.Middlewares
{
    public class ExceptionhandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionhandlingMiddleware> logger;
        public ExceptionhandlingMiddleware(RequestDelegate next, ILogger<ExceptionhandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext).ConfigureAwait(false);
            }
            catch (System.Exception ex)
            {
                if(!httpContext.Response.HasStarted)
                {
                    logger.LogError(ex,"Request Error");
                    httpContext.Response.StatusCode=(int)StatusCodes.Status200OK;
                    httpContext.Response.ContentType="application/json";
                    var response= new ServiceResponse<string>();
                    response.SetException(ex);
                    var json = JsonConvert.SerializeObject(response);
                    await httpContext.Response.WriteAsync(json);
                }
            }
        }

    }
}