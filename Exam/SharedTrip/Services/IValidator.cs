using SharedTrip.Models.Trips;
using SharedTrip.Models.Users;
using System.Collections.Generic;

namespace SharedTrip.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUserRegistration(UserRegistrationFormModel model);

        ICollection<string> ValidateTripCreation(AddTripFormModel model);
    }
}
