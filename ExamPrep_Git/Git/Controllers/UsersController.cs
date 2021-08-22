namespace Git.Controllers
{
    using System.Linq;
    using Git.Models.Users;
    using Git.Services;
    using Git.Services.Users;

    using MyWebServer.Controllers;
    using MyWebServer.Http;

    public class UsersController : Controller
    {
        private readonly IValidator validator;
        private readonly IPasswordHasher passwordHasher;
        private readonly IUserService userService;

        public UsersController(IValidator validator, IPasswordHasher passwordHasher, IUserService userService)
        {
            this.validator = validator;
            this.passwordHasher = passwordHasher;
            this.userService = userService;
        }
       
        public HttpResponse Register()
        {
            if (User.IsAuthenticated)
            {
                return BadRequest();
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterUserFormModel model)
        {
            if (User.IsAuthenticated)
            {
                return BadRequest();
            }

            var modelErrors = this.validator.ValidateUserRegistration(model);

            if (!this.userService.IsEmailAvailable(model.Email))
            {
                modelErrors.Add($"There is already a user with this e-mail.");
            }

            if (!this.userService.IsUsernameAvailable(model.Username))
            {
                modelErrors.Add($"There is already a user with this username.");
            }

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            this.userService.CreateUser(
                model.Username,
                model.Email,
                passwordHasher.HashPassword(model.Password));

            return Redirect("/Users/Login");
        }

        public HttpResponse Login()
        {
            if (User.IsAuthenticated)
            {
                return BadRequest();
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Login(LoginUserFormModel model)
        {
            if (User.IsAuthenticated)
            {
                return BadRequest();
            }

            var hashedPassword = this.passwordHasher.HashPassword(model.Password);

            var userId = this.userService.GetUserId(model.Username, hashedPassword);

            if (userId == null)
            {
                return Error("Invalid username or password.");
            }

            this.SignIn(userId);

            return Redirect("/Repositories/All");
        }

        [Authorize]
        public HttpResponse Logout()
        {
            this.SignOut();

            return Redirect("/");
        }
    }
}
