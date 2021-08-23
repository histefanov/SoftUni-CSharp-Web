namespace CarShop.Services.Cars
{
    using System.Collections.Generic;
    using CarShop.Models.Cars;

    public interface ICarService
    {
        IEnumerable<CarListingViewModel> All(string userId);

        void Create(string model, int year, string pictureUrl, string plateNumber, string ownerId);

        bool CarBelongsToUser(string carId, string userId);
    }
}
