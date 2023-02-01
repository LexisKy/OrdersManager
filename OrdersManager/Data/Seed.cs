using Microsoft.AspNetCore.Identity;
using OrdersManager.Data.Enum;
using OrdersManager.Models;

namespace OrdersManager.Data;

public class Seed
{
    
 /*    public static void SeedData(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.EnsureCreated();

            if (!context.Orders.Any())
            {
                context.Orders.AddRange(new List<Order>()
                    {
                        new Order()
                        {
                            Client = new Client()
                            {
                                loginInfo = "098@gmail.com",
                                passwordInfo = "0978542",
                                vpnInfo = "USA",
                                note = "Small and fast order"
                            },
                            Shop = new Shop()
                            {
                                Name = "Overgear",
                                Url = "Overgear.com",
                                Image = "/Overgear.jpg"
                            },
                            StartDate = new DateTime(2022, 7, 20, 18, 30, 25),
                            EndTime = new DateTime(2022, 8, 21, 18, 30, 25),
                            Payment = 171,
                            status = Status.Done,
                            Description = "70-80 Warlock"
                        },
                        new Order()
                        {
                            Client = new Client()
                            {
                                loginInfo = "098123@gmail.com",
                                passwordInfo = "09785424164316",
                                vpnInfo = "USA",
                                note = "New order"
                            },
                            Shop = new Shop()
                            {
                                Name = "LfCarry",
                                Url = "LfCarry.com",
                                Image = "/LfCarry.jpg"
                            },
                            StartDate = new DateTime(2022, 7, 20, 18, 30, 25),
                            EndTime = new DateTime(2022, 8, 21, 18, 30, 25),
                            Payment = 171,
                            status = Status.Done,
                            Description = "1-80 Mage Super fast"
                        },
                        new Order()
                        {
                            Booster = new AppUser()
                            {
                                Name = "Akmo",
                            },
                            Client = new Client()
                            {
                                loginInfo = "1872@gmail.com",
                                passwordInfo = "164316",
                                vpnInfo = "USA",
                                note = "New small order"
                            },
                            Shop = new Shop()
                            {
                                Name = "Funpay",
                                Url = "Funpay.com",
                                Image = "/Funpay.jpg"
                            },
                            StartDate = new DateTime(2022, 7, 20, 18, 30, 25),
                            EndTime = new DateTime(2022, 8, 21, 18, 30, 25),
                            Payment = 171,
                            status = Status.Done,
                            Description = "15-30 Warrior"
                        }
                    });
                context.SaveChanges();
            }
        }
    }*/

    public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            //Roles
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            if (!await roleManager.RoleExistsAsync(UserRoles.Block))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Block));

            /*//Users
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            string adminUserEmail = "alekseizainullin@gmail.com";

            var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
            if (adminUser == null)
            {
                var newAdminUser = new AppUser()
                {
                    UserName = "LexisKky",
                    Email = adminUserEmail,
                    EmailConfirmed = true,
                    

                };
                await userManager.CreateAsync(newAdminUser, "092309Lexis!");
                await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
            }

            string appUserEmail = "user@gmail.com";

            var appUser = await userManager.FindByEmailAsync(appUserEmail);
            if (appUser == null)
            {
                var newAppUser = new AppUser()
                {
                    UserName = "Grim",
                    Email = appUserEmail,
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(newAppUser, "092309Lexis!");
                await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
            }*/
        }
    }
}