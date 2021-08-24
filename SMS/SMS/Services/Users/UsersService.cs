namespace SMS.Services.Users
{
    using System.Linq;
    using SMS.Data;
    using SMS.Data.Models;

    public class UsersService : IUsersService
    {
        private readonly SMSDbContext data;

        public UsersService(SMSDbContext data)
            => this.data = data;

        public void Create(string username, string email, string password, string cartId)
        {
            this.data
                .Users
                .Add(new User
                {
                    Username = username,
                    Email = email,
                    Password = password,
                    CartId = cartId
                });

            this.data.SaveChanges();
        }

        public bool IsUsernameAvailable(string username)
            => !this.data
                .Users
                .Any(u => u.Username == username);

        public string GetUserId(string username, string password)
           => this.data
                .Users
                .Where(u => u.Username == username && u.Password == password)
                .Select(u => u.Id)
                .FirstOrDefault();
    }
}
