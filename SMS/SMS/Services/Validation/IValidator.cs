namespace SMS.Services.Validation
{
    using System.Collections.Generic;
    using SMS.Models.Products;
    using SMS.Models.Users;

    public interface IValidator
    {
        ICollection<string> ValidateUser(UserRegistrationFormModel model);

        ICollection<string> ValidateProduct(ProductCreateFormModel model);
    }
}
