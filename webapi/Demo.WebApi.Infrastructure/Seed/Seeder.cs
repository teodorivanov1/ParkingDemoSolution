using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.WebApi.Infrastructure.Seed
{
    // I never pay attention when I write planters, but some verification is needed here.
    // If wrong email is provided nothing happens (silently).
    public class Seeder
    {
        public static string CreateDefaultUser(
            IServiceProvider serviceProvider,
            string password,
            string email)
        {
            var userManager = serviceProvider.GetService<UserManager<AppUser>>() ?? throw new InvalidOperationException(nameof(serviceProvider));

            // tradeoff with non-async invocations
            var user = userManager.FindByEmailAsync(email).Result;
            if (user is null)
            {
                user = new AppUser
                {
                    Email = email,
                    UserName = email,
                    EmailConfirmed = true
                };

                userManager.CreateAsync(user, password).Wait();
            }

            if (user is null)
            {
                throw new InvalidOperationException("Unable to read or create user.");
            }

            return user.Id;
        }
    }
}
