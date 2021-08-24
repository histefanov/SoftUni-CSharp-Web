namespace SMS
{
    using System.Threading.Tasks;
    using SMS.Data;
    using SMS.Services;
    using SMS.Services.Validation;
    using SMS.Services.Users;
    using SMS.Services.Carts;
    using SMS.Services.Products;
    using Microsoft.EntityFrameworkCore;

    using MyWebServer;
    using MyWebServer.Controllers;
    using MyWebServer.Results.Views;

    public class StartUp
    {
        public static async Task Main()
            => await HttpServer
                .WithRoutes(routes => routes
                    .MapStaticFiles()
                    .MapControllers())
                .WithServices(services => services
                    .Add<IViewEngine, CompilationViewEngine>()
                    .Add<SMSDbContext>()
                    .Add<IValidator, Validator>()
                    .Add<IPasswordHasher, PasswordHasher>()
                    .Add<IUsersService, UsersService>()
                    .Add<ICartsService, CartsService>()
                    .Add<IProductsService, ProductsService>())
                .WithConfiguration<SMSDbContext>(context => context
                    .Database.Migrate())
                .Start();
    }
}