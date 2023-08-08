using HadisIelts.Server.Data;
using Microsoft.AspNetCore.Identity;

namespace HadisIelts.Server.Models
{
    public class InitializeUsersRoles
    {
        private readonly static List<string> _roles = new List<string>() { "Administrator", "Teacher", "Student" };
        private readonly static List<(ApplicationUser User, string Password)> _users =
            new(){
                (new ApplicationUser
                {
                    FirstName = "Hadis",
                    LastName ="Rajabi",
                    DateOfBirth = new DateTime(1994,9,7),
                    Email="hadisrajabienglishclass@gmail.com",
                    EmailConfirmed = true,
                    Skype="live:.cid.f8c7c899150de7eb",
                    UserName="hadisrajabienglishclass@gmail.com",
                }, "\t?w??RJ??+??\u001b7\f[N???]?_????6??MF"),
                (new ApplicationUser
                {
                    FirstName = "Siavash",
                    LastName ="Nabipour",
                    DateOfBirth = new DateTime(1994,1,5),
                    Email="siavashnabipour@gmail.com",
                    EmailConfirmed = true,
                    UserName="siavashnabipour@gmail.com",
                },"Z?6F?kY?9P????g/???-wHn?\u0018???cvJ?")
            };
        public static async Task Initialize(IServiceScope serviceProvider)
        {
            try
            {
                var roleManager = serviceProvider.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                var dbContext = serviceProvider.ServiceProvider.GetService<ApplicationDbContext>();
                await CreateRoles(roleManager, _roles);
                await CreateDefaultUsers(userManager!, _users);
                var userRoles = new List<(ApplicationUser User, List<string> Roles)>
                {
                    (_users[0].User , new List<string>{_roles[0],_roles[2]}),
                    (_users[1].User , new List<string>{_roles[0],_roles[1]}),
                };
                await AssignRoles(userManager, userRoles);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static async Task CreateRoles(RoleManager<IdentityRole> roleManager, List<string> roles)
        {
            foreach (var role in roles)
            {
                var roleExists = roleManager.RoleExistsAsync(role);
                if (!roleExists.Result)
                {
                    var aspRole = new IdentityRole(role);
                    await roleManager.CreateAsync(aspRole);
                }
            }
        }

        private static async Task CreateDefaultUsers(UserManager<ApplicationUser> userManager
            , List<(ApplicationUser User, string Password)> users)
        {
            foreach (var user in users)
            {
                var userExists = await userManager.FindByEmailAsync(user.User.Email!);
                if (userExists is null)
                {
                    await userManager.CreateAsync(user.User, user.Password);
                }
            }
        }

        private static async Task AssignRoles(UserManager<ApplicationUser> userManager
            , List<(ApplicationUser User, List<string> Roles)> userRoles)
        {
            foreach (var user in userRoles)
            {
                foreach (var role in user.Roles)
                {
                    var userIsInRole = userManager.IsInRoleAsync(user.User, role);
                    if (!userIsInRole.Result)
                    {
                        await userManager.AddToRoleAsync(user.User, role);
                    }
                }
            }
        }
    }
}
