using System.Security.Claims;

namespace Application.Common.Services
{
    public class UserInfo : IUserInfo
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public UserInfo(IHttpContextAccessor httpContextAccessor)
        {

            _httpContextAccessor = httpContextAccessor
                ?? throw new ArgumentNullException(nameof(httpContextAccessor));

            var currentContext = _httpContextAccessor.HttpContext;
            if (currentContext == null || !currentContext.User.Identity.IsAuthenticated)
            {
                UserId = "n/a";
                FirstName = "n/a";
                LastName = "n/a";
                return;
            }

            UserId = _httpContextAccessor.HttpContext?
                        .User?
                        .FindFirstValue(ClaimTypes.NameIdentifier) ?? "n/a";
            FirstName = _httpContextAccessor.HttpContext?
                        .User?
                        .FindFirstValue(ClaimTypes.GivenName) ?? "n/a";
            LastName = _httpContextAccessor.HttpContext?
                        .User?
                        .FindFirstValue(ClaimTypes.Surname) ?? "n/a";
        }
    }
}
