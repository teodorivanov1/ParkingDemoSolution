using Demo.WebApi.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Demo.WebApi.ServiceHost.Authorization
{
    public interface IAuthService
    {
        string CreateToken(AppUser user);
        bool IsAuthenticated(UserManager<AppUser> userManager, AuthRequest request, out AppUser? appUser);
        Task PersistToken();
    }
}