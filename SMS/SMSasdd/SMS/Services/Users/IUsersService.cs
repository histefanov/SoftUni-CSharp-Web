namespace SMS.Services.Users
{
    public interface IUsersService
    {
        void Create(string username, string email, string password, string cartId);

        bool IsUsernameAvailable(string username);

        string GetUserId(string username, string password);
    }
}
