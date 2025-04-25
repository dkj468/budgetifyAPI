﻿using budgetifyAPI.Exceptions;
using System.Net;
using System.Text.Json;

namespace budgetifyAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
               
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex switch
                {
                    BadHttpRequestException => (int) HttpStatusCode.BadRequest,
                    _ => (int) HttpStatusCode.InternalServerError
                };

                var response = _env.IsDevelopment()
                                  ? new AppException { StatusCode = context.Response.StatusCode, ErrorMessage = ex.Message, Detail = ex.StackTrace }
                                  : new AppException { StatusCode = context.Response.StatusCode, ErrorMessage = ex.Message, Detail = "" };
                var jsonResponse = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(jsonResponse);
            }               
        }

    }
}
