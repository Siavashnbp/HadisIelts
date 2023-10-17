using HadisIelts.Server.Data;
using HadisIelts.Server.Models;
using HadisIelts.Shared.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using static HadisIelts.Shared.Enums.UserRelatedEnums;

namespace HadisIelts.Server.Services.User
{
    public class UserServicesProvider : IUserServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        public UserServicesProvider(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public List<ApplicationUser> FindUsers(string? searchPhrase)
        {
            if (searchPhrase is not null)
            {
                var users = _dbContext.Users.ToList().FindAll(x => x.UserName!.Contains(searchPhrase)
                || x.FirstName.Contains(searchPhrase) || x.LastName.Contains(searchPhrase));
                return users;
            }
            return _dbContext.Users.ToList();
        }

        public string GetUserIdFromClaims(List<Claim> claims)
        {
            return claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
        }

        public async Task<UserInformationSharedModel> GetUserInformationAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return new UserInformationSharedModel(id: user.Id, username: user.UserName!, email: user.Email!)
                {
                    Birthday = DateOnly.FromDateTime(user.DateOfBirth!.Value),
                    FirstName = user.FirstName!,
                    LastName = user.LastName!,
                    Skype = user.Skype,
                };
            }
            return null!;
        }

        public async Task<List<Tuple<ApplicationRoles, bool>>> GetUserRolesAsync(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            return ConvertToApplicationRoles(userRoles.ToList());
        }

        public async Task<List<UserRolesSharedModel>> GetUsersRolesAsync(List<ApplicationUser> users)
        {
            var usersRoles = new List<UserRolesSharedModel>();
            if (users is not null && users.Count > 0)
            {
                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var applicationRoles = ConvertToApplicationRoles(roles.ToList());
                    usersRoles.Add(new UserRolesSharedModel(id: user.Id, email: user.Email!)
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Roles = applicationRoles,
                    });
                }
            }
            return usersRoles;
        }


        public bool HasWritingCorrectionPending(ApplicationDbContext dbContext, string userId)
        {
            return _dbContext.WritingCorrectionSubmissionGroups.Where(x => x.UserId == userId).Any(x => !x.IsCorrected);
        }

        public bool IsUserOwnerOrSpecificRoles(List<Claim> claims, List<string> roles, string userId)
        {
            //is user owner
            var isUserOwner = GetUserIdFromClaims(claims) == userId;
            if (isUserOwner)
            {
                return true;
            }
            //is user in roles
            return claims.Any(claim => claim.Type == "role" && roles.Contains(claim.Value));
        }

        private List<Tuple<ApplicationRoles, bool>> ConvertToApplicationRoles(List<string> roles)
        {
            var applicationRoles = new List<Tuple<ApplicationRoles, bool>>
            {
                Tuple.Create(ApplicationRoles.Administrator, roles.Contains("Administrator")),
                Tuple.Create(ApplicationRoles.Teacher, roles.Contains("Teacher")),
                Tuple.Create(ApplicationRoles.Student, roles.Contains("Student"))
            };
            return applicationRoles;
        }
    }
}
