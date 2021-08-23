namespace CarShop.Services.Users
{
    using System.Linq;
    using CarShop.Data;
    using CarShop.Data.Models;

    using static Data.DataConstants;

    public class UserService: IUserService
    {
        private readonly CarShopDbContext data;

        public UserService(CarShopDbContext data) 
            => this.data = data;

        public void Create(string username, string email, string password, string userType)
        {
            this.data
                .Users
                .Add(new User
                {
                    Username = username,
                    Email = email,
                    Password = password,
                    IsMechanic = userType == MechanicUserType
                });

            this.data.SaveChanges();
        }

        public string GetUserId(string username, string password)
           => this.data
                .Users
                .Where(u => u.Username == username && u.Password == password)
                .Select(u => u.Id)
                .FirstOrDefault();

        public bool IsUserMechanic(string userId)
            => this.data
                .Users
                .Any(u => u.Id == userId && u.IsMechanic);

        public bool IsUsernameAvailable(string username)
            => !this.data
                .Users
                .Any(u => u.Username == username);
    }
}
