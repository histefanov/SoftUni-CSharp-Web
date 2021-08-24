namespace SMS.Controllers
{
    using System.Linq;
    using SMS.Services;
    using SMS.Services.Validation;
    using SMS.Services.Users;
    using SMS.Models.Users;
    using SMS.Services.Carts;

    using MyWebServer.Controllers;
    using MyWebServer.Http;

    using static Services.Validation.ErrorMessages.Users;

    public class UsersController : Controller
    {
        private readonly IValidator validator;
        private readonly IPasswordHasher passwordHasher;
        private readonly IUsersService usersService;
        private readonly ICartsService cartsService;

        public UsersController(
            IValidator validator,
            IPasswordHasher passwordHasher,
            IUsersService usersService, 
            ICartsService cartsService)
        {
            this.validator = validator;
            this.passwordHasher = passwordHasher;
            this.usersService = usersService;
            this.cartsService = cartsService;
        }

        public HttpResponse Register()
        {
            if (this.User.IsAuthenticated)
            {
                return BadRequest();
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Register(UserRegistrationFormModel model)
        {
            if (this.User.IsAuthenticated)
            {
                return BadRequest();
            }

            var modelErrors = this.validator.ValidateUser(model);

            if (!usersService.IsUsernameAvailable(model.Username))
            {
                modelErrors.Add(UsernameUnavailableMessage);
            }

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var cartId = this.cartsService.Create();

            this.usersService.Create(
                model.Username,
                model.Email,
                this.passwordHasher.HashPassword(model.Password),
                cartId);

            return Redirect("/Users/Login");
        }

        public HttpResponse Login()
        {
            if (this.User.IsAuthenticated)
            {
                return BadRequest();
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Login(UserLoginFormModel model)
        {
            var userId = this.usersService.GetUserId(
                model.Username,
                this.passwordHasher.HashPassword(model.Password));

            if (userId == null)
            {
                return Error(InvalidCredentialsMessage);
            }

            this.SignIn(userId);

            return Redirect("/Home/IndexLoggedIn");
        }

        [Authorize]
        public HttpResponse Logout()
        {
            this.SignOut();

            return Redirect("/");
        }
    }
}
