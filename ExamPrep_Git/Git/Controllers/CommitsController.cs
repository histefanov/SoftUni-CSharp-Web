namespace Git.Controllers
{
    using Git.Models.Commits;
    using Git.Services;
    using Git.Services.Commits;
    using Git.Services.Repositories;

    using MyWebServer.Controllers;
    using MyWebServer.Http;

    using static Data.DataConstants.Commit;

    public class CommitsController : Controller
    {
        private readonly IValidator validator;
        private readonly ICommitService commitService;
        private readonly IRepositoryService repositoryService;

        public CommitsController(IValidator validator, ICommitService commitService, IRepositoryService repositoryService)
        {
            this.validator = validator;
            this.commitService = commitService;
            this.repositoryService = repositoryService;
        }

        [Authorize]
        public HttpResponse All()
        {
            var commits = this.commitService.All(User.Id);

            return View(commits);
        }

        public HttpResponse Create(string id)
        {
            if (!this.repositoryService.IsPublic(id))
            {
                return BadRequest();
            }

            var repositoryForCommit = this.repositoryService.GetRepositoryForCommit(id);

            if (repositoryForCommit == null)
            {
                return BadRequest();
            }

            if (!User.IsAuthenticated)
            {
                return Redirect("/Users/Login");
            }

            return View(repositoryForCommit);
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Create(CreateCommitFormModel model)
        {
            if (!this.repositoryService.IsPublic(model.Id))
            {
                return BadRequest();
            }

            if (!this.validator.IsCommitDescriptionValid(model.Description))
            {
                return Error($"Description must be at least {DescriptionMinLength} character long.");
            }

            var isSuccess = this.commitService.Create(
                User.Id, 
                model.Id, 
                model.Description);

            if (!isSuccess)
            {
                return NotFound();
            }

            return Redirect("/Repositories/All");
        }

        [Authorize]
        public HttpResponse Delete(string id)
        {
            var isSuccess = this.commitService.Delete(User.Id, id);

            if (!isSuccess)
            {
                return BadRequest();
            }

            return Redirect("/Commits/All");
        }
    }
}
