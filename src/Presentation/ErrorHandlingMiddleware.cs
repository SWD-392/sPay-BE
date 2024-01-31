using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Presentation.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly List<string> _allowedMethods;

    public ErrorHandlingMiddleware(RequestDelegate next, List<string> allowedMethods)
    {
        _next = next;
        _allowedMethods = allowedMethods ?? new List<string>();
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine("ex:" + ex.Message);

            // Kiểm tra nếu phương thức của request thuộc danh sách được phép xử lý lỗi, thì mới thực hiện xử lý lỗi
            if (_allowedMethods.Count == 0 || _allowedMethods.Contains(context.Request.Method.ToUpper()))
            {
                var errorDetails = GetErrorDetails(ex, context);

                context.Response.StatusCode = errorDetails.Status;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorDetails));
            }
            else
            {
                throw; // Cho phép xử lý lỗi tiếp tục cho các loại request khác
            }
        }
    }

    private object GetErrorDetails(Exception ex, HttpContext context)
    {
        if (ex is ValidationException validationException)
        {
            // Xử lý lỗi kiểm tra hợp lệ
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return new
            {
                errors = validationException.Errors,
                type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                title = "One or more validation errors occurred.",
                status = (int)HttpStatusCode.BadRequest,
                traceId = context.TraceIdentifier
            };
        }
        else if (ex is Exception customException)
        {
            // Xử lý lỗi tùy chỉnh
            return new
            {
                title = "Internal Server Error",
                message = customException.Message,
                status = (int)HttpStatusCode.InternalServerError,
            };
        }
        else
        {
            // Xử lý các trường hợp lỗi khác
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return new
            {
                title = "Internal Server Error",
                message = customException.Message,
                status = (int)HttpStatusCode.InternalServerError,
            };
        }
    }
}
