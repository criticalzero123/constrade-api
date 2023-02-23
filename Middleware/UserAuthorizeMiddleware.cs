using ConstradeApi.Model.MUserAuthorize.Repository;

namespace ConstradeApi.Middleware
{
    public class UserAuthorizeMiddleware
    {
        private readonly RequestDelegate _next;

        public UserAuthorizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserAuthorizeRepository sessionRepository)
        {
            try
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(',').Last();
                if (token == null)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }

                var session = await sessionRepository.GetApiKeyAsync(token);
                if (session != null)
                {
                    context.Items["UserId"] = session.UserId;
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
                Console.WriteLine($"Hello");
                await _next(context);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

      
    }
}
