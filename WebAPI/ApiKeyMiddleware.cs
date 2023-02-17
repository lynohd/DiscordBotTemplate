using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.WebAPI;
public class ApiKeyMiddleware : IMiddleware
{
    private readonly IConfiguration _config;
    private readonly ILogger<ApiKeyMiddleware> _logger;

    public ApiKeyMiddleware(IConfiguration config, ILogger<ApiKeyMiddleware> logger)
    {
        _config = config;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if(!context.Request.Headers.TryGetValue("x-api-key", out var key))
        {
            _logger.LogInformation("{ErrorCode}: Request missing API key.", 401);
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API key missing");
            return;
        }

        if(!key.Equals(_config["ApiKey"]))
        {
            _logger.LogInformation("{ErrorCode}: Request has invalid API key {Key}", 401, key);
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Wrong API key");
            return;
        }
        await next(context);
    }
}
