using System.Security.Claims;

namespace Application.Common.Services
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string UserId => _httpContextAccessor.HttpContext?
                        .User?
                        .FindFirstValue(ClaimTypes.NameIdentifier) ?? "n/a";

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor
                ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
    }
}
