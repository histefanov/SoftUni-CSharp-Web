namespace Git
{
    using System.Threading.Tasks;
    using Git.Data;
    using Git.Services;
    using Git.Services.Commits;
    using Git.Services.Repositories;
    using Git.Services.Users;
    using Microsoft.EntityFrameworkCore;

    using MyWebServer;
    using MyWebServer.Controllers;
    using MyWebServer.Results.Views;

    public class Startup
    {
        public static async Task Main()
            => await HttpServer
                .WithRoutes(routes => routes
                    .MapStaticFiles()
                    .MapControllers())
                .WithServices(services => services
                    .Add<GitDbContext>()
                    .Add<IViewEngine, CompilationViewEngine>()
                    .Add<IValidator, Validator>()
                    .Add<IPasswordHasher, PasswordHasher>()
                    .Add<IUserService, UserService>()
                    .Add<IRepositoryService, RepositoryService>()
                    .Add<ICommitService, CommitService>())
                .WithConfiguration<GitDbContext>(context => context
                    .Database.Migrate())
                .Start();
    }
}
