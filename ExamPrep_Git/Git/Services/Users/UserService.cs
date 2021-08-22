namespace Git.Services.Users
{
    using System.Linq;
    using Git.Data;
    using Git.Data.Models;

    public class UserService : IUserService
    {
        private readonly GitDbContext data;

        public UserService(GitDbContext data) 
            => this.data = data;

        public string CreateUser(string username, string email, string password)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                Password = password
            };

            this.data.Users.Add(user);
            this.data.SaveChanges();

            return user.Id;
        }

        public string GetUserId(string username, string password)
        {
            var userId = this.data
                .Users
                .Where(u => u.Username == username && u.Password == password)
                .Select(u => u.Id)
                .FirstOrDefault();

            return userId;
        }

        public bool IsEmailAvailable(string email)
            => !this.data
                .Users
                .Any(u => u.Email == email);

        public bool IsUsernameAvailable(string username)
            => !this.data
                .Users
                .Any(u => u.Username == username);
    }
}
