namespace Git.Controllers
{
    using System.Linq;
    using Git.Models.Repositories;
    using Git.Services;
    using Git.Services.Repositories;

    using MyWebServer.Controllers;
    using MyWebServer.Http;

    public class RepositoriesController : Controller
    {
        private readonly IValidator validator;
        private readonly IRepositoryService repositoryService;

        public RepositoriesController(IValidator validator, IRepositoryService repositoryService)
        {
            this.validator = validator;
            this.repositoryService = repositoryService;
        }

        public HttpResponse All() 
        {
            var userId = User.IsAuthenticated ? User.Id : null;

            var repositories = this.repositoryService.All(userId);

            return View(repositories);
        }

        public HttpResponse Create() 
        {
            if (!User.IsAuthenticated)
            {
                return Redirect("/Users/Login");
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Create(CreateRepositoryFormModel model)
        {
            var modelErrors = this.validator.ValidateRepositoryCreating(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            this.repositoryService.Create(
                User.Id, 
                model.Name,
                model.RepositoryType);

            return Redirect("/Repositories/All");
        }
    }
}
