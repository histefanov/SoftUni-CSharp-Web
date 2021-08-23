namespace CarShop.Services
{
    using System.Collections.Generic;
    using CarShop.Models.Cars;
    using CarShop.Models.Users;

    public interface IValidator
    {
        ICollection<string> ValidateUser(UserRegistrationFormModel model);

        ICollection<string> ValidateCar(CarAddFormModel model);

        bool IsIssueDescriptionValid(string description);
    }
}
