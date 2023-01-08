namespace Application.Common.Services
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task CreateUserAsync(
            string userName,
            string password);

        Task DeleteUserAsync(string userId);
    }
}
