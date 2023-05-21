using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace UI.Pages
{
    public partial class UserEditor
    {
        [Parameter]
        public string UserId { get; set; } = null!;

        [Inject]
        public UsersClient UsersClient { get; set; } = null!;

        public UserDetailsViewModel? Model { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            Model = await UsersClient.GetUserAsync(UserId);
        }
    }
}
