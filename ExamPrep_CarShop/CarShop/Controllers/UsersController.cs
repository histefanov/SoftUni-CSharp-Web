namespace CarShop.Controllers
{
    using CarShop.Models.Users;
    using CarShop.Services;
    using CarShop.Services.Users;
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using System.Linq;

    public class UsersController : Controller
    {
        private readonly IValidator validator;
        private readonly IPasswordHasher passwordHasher;
        private readonly IUserService userService;

        public UsersController(
            IValidator validator,
            IPasswordHasher passwordHasher,
            IUserService userService)
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
        public HttpResponse Register(UserRegistrationFormModel model)
        {
            if (User.IsAuthenticated)
            {
                return BadRequest();
            }

            var modelErrors = this.validator.ValidateUser(model);

            if (!userService.IsUsernameAvailable(model.Username))
            {
                modelErrors.Add("Username is already taken.");
            }

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            this.userService.Create(
                model.Username,
                model.Email,
                this.passwordHasher.HashPassword(model.Password),
                model.UserType);

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
        public HttpResponse Login(UserLoginFormModel model)
        {
            var userId = this.userService.GetUserId(
                model.Username,
                this.passwordHasher.HashPassword(model.Password));

            if (userId == null)
            {
                return Error("Invalid username or password");
            }

            this.SignIn(userId);

            return Redirect("/Cars/All");
        }

        [Authorize]
        public HttpResponse Logout()
        {
            this.SignOut();

            return Redirect("/");
        }
    }
}
