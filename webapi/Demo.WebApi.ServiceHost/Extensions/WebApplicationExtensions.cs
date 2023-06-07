using Demo.WebApi.Infrastructure;
using Demo.WebApi.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;

namespace Demo.WebApi.ServiceHost.Extensions
{
    public static class WebApplicationExtensions
    {
        // This is not a good production (real application) approach.
        // I chose it so as not to complicate the demo with an external initialization of the db (Docker)
        public static void InitializeDb(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
            Seeder.CreateDefaultUser(services, "123", "op1@ps.com");
        }

        public static void AddBoilerplate(this WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllers();
            app.MapDefaultControllerRoute();
        }
    }
}
