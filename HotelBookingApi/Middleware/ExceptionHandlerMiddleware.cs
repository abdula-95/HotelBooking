using System.Net;
using HotelBookingApi.Exceptions;
using Newtonsoft.Json;

namespace HotelBookingApi.Middleware;

public class ExceptionHandlerMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var code = ex switch
            {
                NotFoundException => HttpStatusCode.NotFound,
                ConflictException => HttpStatusCode.Conflict,
                _ => HttpStatusCode.InternalServerError
            };
            response.StatusCode = (int)code;
            await response.WriteAsync(JsonConvert.SerializeObject(new { error = ex.Message }));
        }
    }
}