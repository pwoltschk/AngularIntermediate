
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
            Model = new UsersViewModel
            {
                Users = new List<UserDto>
                {
                    new UserDto { Name = "John Doe", Email = "john.doe@gmail.com" },
                    new UserDto { Name = "Max Mustermann", Email = "max.musterman@gmail.com" }
                }
            };
        }
    }
}
