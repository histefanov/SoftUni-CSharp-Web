namespace CarShop.Controllers
{
    using System.Linq;
    using CarShop.Models.Cars;
    using CarShop.Services;
    using CarShop.Services.Cars;
    using CarShop.Services.Users;

    using MyWebServer.Controllers;
    using MyWebServer.Http;

    public class CarsController : Controller
    {
        private readonly IValidator validator;
        private readonly IUserService userService;
        private readonly ICarService carService;

        public CarsController(
            IValidator validator, 
            ICarService carService,
            IUserService userService)
        {
            this.validator = validator;
            this.carService = carService;
            this.userService = userService;
        }

        [Authorize]
        public HttpResponse All()
        {
            var cars = this.carService.All(this.User.Id);

            return View(cars);
        }

        [Authorize]
        public HttpResponse Add()
        {
            if (this.userService.IsUserMechanic(this.User.Id))
            {
                return BadRequest();
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Add(CarAddFormModel model)
        {
            var userId = this.User.Id;

            if (this.userService.IsUserMechanic(userId))
            {
                return BadRequest();
            }

            var modelErrors = this.validator.ValidateCar(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            this.carService.Create(
                model.Model,
                model.Year,
                model.Image,
                model.PlateNumber,
                userId);

            return Redirect("/Cars/All");
        }
    }
}
