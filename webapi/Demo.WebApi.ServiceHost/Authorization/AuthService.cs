using Demo.WebApi.Infrastructure;
using Demo.WebApi.ServiceHost.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Demo.WebApi.ServiceHost.Authorization
{
    public class AuthService : IAuthService
    {
        private const int ExpirationMinutes = 30;
        private const string JwtSub = "TokenForTheApiWithAuth";
        private readonly AppDbContext dbContext;
        public AuthService(AppDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        // TODO is better to provide some validation ruleset and do self-validation, but i will skip this for now.
        // Тhe biggest problem here is that we use the db context from the service host.
        // This is unacceptable for this architecture. I leave it as it is with the proviso that has been noticed.
        public bool IsAuthenticated(UserManager<AppUser> userManager, AuthRequest request, out AppUser? appUser)
        {
            appUser = null;
            var managedUser = userManager.FindByEmailAsync(request.Email).Result;

            if (managedUser == null)
            {
                return false;
            }

            var isPasswordValid = userManager.CheckPasswordAsync(managedUser, request.Password).Result;

            if (!isPasswordValid)
            {
                return false;
            }

            appUser = dbContext.Users.FirstOrDefault(u => u.Email == request.Email);

            return appUser is not null;
        }

        public async Task PersistToken()
        {
            await dbContext.SaveChangesAsync();
        }

        public string CreateToken(AppUser user)
        {
            var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);

            var token = CreateJwtToken(
                CreateClaims(user),
                CreateSigningCredentials(),
                expiration
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private static JwtSecurityToken CreateJwtToken(
            List<Claim> claims,
            SigningCredentials credentials,
            DateTime expiration) =>
            new(
                ServiceCollectionExtentions.Issuer,
                ServiceCollectionExtentions.Audience,
                claims,
                expires: expiration,
                signingCredentials: credentials
            );

        private static List<Claim> CreateClaims(IdentityUser user) => new()
        {
            new Claim(JwtRegisteredClaimNames.Sub, JwtSub),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!)
        };

        private static SigningCredentials CreateSigningCredentials() =>
            new(new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(ServiceCollectionExtentions.SecurityKey)),
                SecurityAlgorithms.HmacSha256);
    }
}

