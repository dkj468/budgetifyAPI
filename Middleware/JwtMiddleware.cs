
using budgetifyAPI.Repository.Users;
using budgetifyAPI.Services;
using System.Security.Claims;

namespace budgetifyAPI.Middleware
{
    public class JwtMiddleware
    {
        private TokenService _tokenService;
        private readonly RequestDelegate _next;
        private IUserRepository _userRepo;
        private readonly ILogger<JwtMiddleware> _logger;
        public JwtMiddleware(RequestDelegate next, ILogger<JwtMiddleware> logger)
        {            
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            // extract token from request header 
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            _logger.LogInformation("JWT", token);
            if (!String.IsNullOrEmpty(token)) 
            {
                try
                {
                    // get token service from DI container 
                    _tokenService = context.RequestServices.GetRequiredService<TokenService>();
                    _userRepo = context.RequestServices.GetRequiredService<IUserRepository>();

                    var claimsPrincipal = _tokenService.ValidateJwtToken(token);
                    var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;                    
                }
                catch (Exception ex)
                {
                    context.Response.WriteAsync(ex.ToString());
                }

                
            }

            await _next(context);
            
        }
    }
}
