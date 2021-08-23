namespace CarShop.Controllers
{
    using CarShop.Models.Issues;
    using CarShop.Services;
    using CarShop.Services.Cars;
    using CarShop.Services.Issues;
    using CarShop.Services.Users;

    using MyWebServer.Controllers;
    using MyWebServer.Http;

    using static Data.DataConstants;

    public class IssuesController : Controller
    {
        private readonly IValidator validator;
        private readonly IIssueService issueService;
        private readonly IUserService userService;
        private readonly ICarService carService;

        public IssuesController(
            IValidator validator, 
            IIssueService issueService, 
            IUserService userService, 
            ICarService carService)
        {
            this.issueService = issueService;
            this.validator = validator;
            this.userService = userService;
            this.carService = carService;
        }

        [Authorize]
        public HttpResponse CarIssues(string carId)
        {
            var userId = this.User.Id;

            if (!this.carService.CarBelongsToUser(carId, userId) &&
                !this.userService.IsUserMechanic(userId))
            {
                return Unauthorized();
            }

            var issues = this.issueService.All(carId);

            return View(issues);
        }

        [Authorize]
        public HttpResponse Add(string carId) => View(carId, viewName: "Add");

        [HttpPost]
        [Authorize]
        public HttpResponse Add(IssueAddViewModel model)
        {
            if (!this.validator.IsIssueDescriptionValid(model.Description))
            {
                return Error($"Description must be at least {IssueDescriptionMinLength} character long.");
            }

            this.issueService.Add(
                model.CarId,
                model.Description);

            return Redirect($"/Issues/CarIssues?carId={model.CarId}");
        }

        [Authorize]
        public HttpResponse Delete(string issueId, string carId)
        {
            var userId = this.User.Id;

            if (!this.userService.IsUserMechanic(userId) && 
                !this.carService.CarBelongsToUser(carId, userId))
            {
                return Unauthorized();
            }

            this.issueService.Delete(issueId, carId);

            return Redirect($"/Issues/CarIssues?carId={carId}");
        }

        [Authorize]
        public HttpResponse Fix(string issueId, string carId)
        {
            if (!this.userService.IsUserMechanic(this.User.Id))
            {
                return BadRequest();
            }

            this.issueService.Fix(issueId, carId);

            return Redirect($"/Issues/CarIssues?carId={carId}");
        }
    }
}
