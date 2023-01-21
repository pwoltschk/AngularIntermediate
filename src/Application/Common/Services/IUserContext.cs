namespace Application.Common.Services
{
    public interface IUserContext
    {
        string UserId { get; }
        string FirstName { get; }
        string LastName { get; }

    }
}
