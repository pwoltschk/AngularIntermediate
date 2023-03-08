using Application.Common;
using Application.Common.Exceptions;
using Application.Common.Services;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user?.UserName;
        }

        public async Task CreateUserAsync(string userName, string password)
        {
            var user = new IdentityUser
            {
                UserName = userName,
                Email = userName,
            };

            await _userManager.CreateAsync(user, password);

        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            await _userManager.DeleteAsync(user);
        }

        public async Task<IList<Role>> GetRolesAsync(CancellationToken cancellationToken)
        {
            var roles = await _roleManager.Roles.ToListAsync(cancellationToken);
            var roleList = new List<Role>();

            foreach (var role in roles)
            {
                var claims = await _roleManager.GetClaimsAsync(role);
                var permissions = claims
                    .Where(c => c.Type == nameof(Permission))
                    .Select(c => c.Value)
                    .ToList();

                roleList.Add(new Role(role.Id, role.Name, permissions));
            }

            return roleList;
        }
    }
}
