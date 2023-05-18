
namespace UI.Pages
{
    public partial class UserManager
    {
        public UsersViewModel Model { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadUsers();
        }

        private async Task LoadUsers()
        {
        }
    }
}
