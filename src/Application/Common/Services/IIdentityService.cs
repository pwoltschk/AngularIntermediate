namespace Application.Common.Services
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task CreateUserAsync(
            string userName,
            string password);

        Task<IList<Role>> GetRolesAsync(CancellationToken cancellationToken);
 
        Task DeleteUserAsync(string userId);
    }
}
