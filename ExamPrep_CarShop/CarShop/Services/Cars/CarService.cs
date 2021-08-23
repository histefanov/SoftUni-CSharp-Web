namespace CarShop.Services.Cars
{
    using System.Collections.Generic;
    using System.Linq;
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.Models.Cars;

    public class CarService : ICarService
    {
        private readonly CarShopDbContext data;

        public CarService(CarShopDbContext data) 
            => this.data = data;

        public IEnumerable<CarListingViewModel> All(string userId)
        {
            var user = this.data
                .Users
                .Find(userId);

            var carsQuery = this.data
                .Cars
                .AsQueryable();

            if (user.IsMechanic)
            {
                carsQuery = carsQuery
                    .Where(c => c.Issues.Any(i => !i.IsFixed));
            }
            else
            {
                carsQuery = carsQuery
                    .Where(c => c.OwnerId == userId);
            }

            var cars = carsQuery
                .Select(c => new CarListingViewModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    Year = c.Year,
                    PictureUrl = c.PictureUrl,
                    PlateNumber = c.PlateNumber,
                    FixedIssues = c.Issues
                        .Where(i => i.IsFixed)
                        .Count(),
                    RemainingIssues = c.Issues
                        .Where(i => !i.IsFixed)
                        .Count()
                })
                .ToList();

            return cars;
        }

        public void Create(string model, int year, string pictureUrl, string plateNumber, string ownerId)
        {
            this.data
                .Cars
                .Add(new Car
                {
                    Model = model,
                    Year = year,
                    PictureUrl = pictureUrl,
                    PlateNumber = plateNumber,
                    OwnerId = ownerId
                });

            this.data.SaveChanges();
        }

        public bool CarBelongsToUser(string carId, string userId)
            => this.data
                .Cars
                .Any(c => c.Id == carId && c.OwnerId == userId);
    }
}
