using Demo.WebApi.Infrastructure;
using Demo.WebApi.ServiceHost.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.WebApi.ServiceHost.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IAuthService authService;

        public AccountController(UserManager<AppUser> userManager, IAuthService authService)
        {
            this.userManager = userManager;
            this.authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
        {
            if (!authService.IsAuthenticated(userManager, request, out var appUser))
            {
                throw new UnauthorizedAccessException();
            }
            var accessToken = authService.CreateToken(appUser!);
            await authService.PersistToken();

            return Ok(new AuthResponse
            {
                Email = appUser!.Email!,
                Token = accessToken,
            });
        }
    }
}
