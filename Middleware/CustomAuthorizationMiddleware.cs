// Purpose: Middleware to handle unauthorized requests.
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace ACRP_API.Middleware;

public class CustomAuthorizationMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
        {
            context.Response.ContentType = "application/json";
            var errorResponse = JsonSerializer.Serialize(new { Message = "Usuario no autenticado" });

            await context.Response.WriteAsync(errorResponse);
        }
    }
}