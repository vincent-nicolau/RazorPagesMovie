using Microsoft.AspNetCore.Identity;

namespace RazorPagesMovie.Helpers
{
    public static class AdminHelper
    {
        public static readonly string ADMIN_ROLE = "Admin";
        public static readonly string ADMIN_EMAIL = "admin@vnvn.com";

        public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(ADMIN_ROLE))
            {
                await roleManager.CreateAsync(new IdentityRole(ADMIN_ROLE));
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string password = "Admin@123";

            if (await userManager.FindByEmailAsync(ADMIN_EMAIL) == null)
            {
                var user = new IdentityUser
                {
                    UserName = ADMIN_EMAIL,
                    Email = ADMIN_EMAIL,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user, ADMIN_ROLE);
            }
        }
    }
}
