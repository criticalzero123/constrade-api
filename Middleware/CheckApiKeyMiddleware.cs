using ConstradeApi.Model.MUserApiKey.Repository;

namespace ConstradeApi.Middleware
{
    public class CheckApiKeyMiddleware
    {
        private readonly RequestDelegate _next;

        public CheckApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IApiKeyRepository apiRepository)
        {
            try
            {
                string? key = context.Request.Headers["ApiKey"];

                if (string.IsNullOrEmpty(key))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("API KEY is missing");
                    return;
                }

                var api = await apiRepository.GetApiKeyByTokenAsync(key);
                if (api == null)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Invalid Api Key or the Key is not active");
                    return;
                }
                await _next(context);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

      
    }
}
