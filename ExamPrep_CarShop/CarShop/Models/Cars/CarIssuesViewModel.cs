namespace CarShop.Models.Cars
{
    using System.Collections.Generic;
    using CarShop.Models.Issues;

    public class CarIssuesViewModel
    {
        public string CarId { get; init; }

        public string CarModel { get; init; }

        public int CarYear { get; init; }

        public IEnumerable<IssuesListingViewModel> Issues { get; set; }
    }
}
