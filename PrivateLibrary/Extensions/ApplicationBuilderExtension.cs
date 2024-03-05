using Microsoft.AspNetCore.Identity;
using PrivateLibrary.Data.Models;

namespace PrivateLibrary.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder SeedAdmin(this IApplicationBuilder app)
        {
            using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

            IServiceProvider services = scopedServices.ServiceProvider;

            UserManager<ApplicationUser> userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole> roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    IdentityRole adminRole = new IdentityRole("Admin");
                    await roleManager.CreateAsync(adminRole);
                }

                if (!await roleManager.RoleExistsAsync("Employee"))
                {
                    IdentityRole agentRole = new IdentityRole("Employee");
                    await roleManager.CreateAsync(agentRole);
                }

                if (!await roleManager.RoleExistsAsync("Reader"))
                {
                    IdentityRole clientRole = new IdentityRole("Reader");
                    await roleManager.CreateAsync(clientRole);
                }

                ApplicationUser? admin = await userManager.FindByNameAsync("admin");
                await userManager.AddToRoleAsync(admin, "Admin");
            })
            .GetAwaiter()
            .GetResult();

            return app;
        }
    }
}
