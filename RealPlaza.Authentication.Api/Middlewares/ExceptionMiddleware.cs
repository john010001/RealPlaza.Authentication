using RealPlaza.Authentication.Application.Wrappers;
using System.Net;
using System.Text;

namespace RealPlaza.Authentication.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        //private readonly IHostEnvironment environment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger/*, IHostEnvironment environment*/)
        {
            this.next = next;
            this.logger = logger;
            //this.environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (FluentValidation.ValidationException ex)
            {
                logger.LogError(ex, ex.Message);
                string resultValidationException = new ResultResponse<IEnumerable<KeyValuePair<string, string>>>(HttpStatusCode.BadRequest)
                {
                    Message = "Se presentaron uno o más errores de validación.",
                    Data = ex.Errors.Select(x => new KeyValuePair<string, string>(x.PropertyName, x.ErrorMessage))
                }.ToString();
                await HandleExceptionAsync(context, (int)HttpStatusCode.BadRequest, resultValidationException);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                string resultException = new ResultResponse<string>(HttpStatusCode.InternalServerError)
                {
                    Message = "Ocurrió un error inesperado.",
                    Data = $"{ex.Message} - {ex.StackTrace}"
                }.ToString();
                await HandleExceptionAsync(context, (int)HttpStatusCode.InternalServerError, resultException);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, int statusCode, string result)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(result, Encoding.UTF8);
        }
    }
}
