namespace SharedTrip.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SharedTrip.Data;
    using SharedTrip.Models;
    using SharedTrip.Models.Users;
    using SharedTrip.Services;
    using System.Linq;

    public class UsersController : Controller
    {
        private readonly IValidator validator;
        private readonly ApplicationDbContext dbContext;
        private readonly IPasswordHasher passwordHasher;

        public UsersController(IValidator validator, ApplicationDbContext dbContext, IPasswordHasher passwordHasher)
        {
            this.validator = validator;
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
        }

        
        public HttpResponse Register() => View();

        [HttpPost]
        public HttpResponse Register(UserRegistrationFormModel model)
        {
            var modelErrors = this.validator.ValidateUserRegistration(model);

            if (this.dbContext.Users.Any(u => u.Username == model.Username))
            {
                modelErrors.Add($"User with username {model.Username} already exists.");
            }

            if (this.dbContext.Users.Any(u => u.Email == model.Email))
            {
                modelErrors.Add($"User with email {model.Email} already exists.");
            }

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var user = new User()
            {
                Username = model.Username,
                Password = this.passwordHasher.HashPassword(model.Password),
                Email = model.Email

            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            return Redirect("/Users/Login");
        }

        public HttpResponse Login() => View();

        [HttpPost]
        public HttpResponse Login(UserLoginFormModel model)
        {
            var hashedPassword = this.passwordHasher.HashPassword(model.Password);

            var userId = this.dbContext
                .Users
                .Where(u => u.Username == model.Username && u.Password == hashedPassword)
                .Select(u => u.Id)
                .FirstOrDefault();

            if (userId == null)
            {
                return Error("Username and password combination is not valid.");
            }

            this.SignIn(userId);

            return Redirect("/Trips/All");
        }

        public HttpResponse Logout()
        {
            this.SignOut();

            return Redirect("/");
        }
    }
}
