using Demo.WebApi.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Demo.WebApi.ServiceHost.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void ApplyDbMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
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
