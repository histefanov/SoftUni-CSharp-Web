namespace CarShop.Services.Issues
{
    using System.Linq;
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.Models.Cars;
    using CarShop.Models.Issues;

    public class IssueService : IIssueService
    {
        private readonly CarShopDbContext data;

        public IssueService(CarShopDbContext data) 
            => this.data = data;
    
        public CarIssuesViewModel All(string carId)
        {
            var car = this.data
                .Cars
                .Where(c => c.Id == carId)
                .Select(c => new CarIssuesViewModel
                {
                    CarId = c.Id,
                    CarModel = c.Model,
                    CarYear = c.Year
                })
                .FirstOrDefault();

            if (car == null)
            {
                return null;
            }

            var issues = this.data
                .Issues
                .Where(i => i.CarId == carId)
                .Select(i => new IssuesListingViewModel
                {
                    Id = i.Id,
                    Description = i.Description,
                    IsFixed = i.IsFixed ? "Yes" : "Not yet"
                })
                .ToList();

            car.Issues = issues;

            return car;
        }

        public void Add(string carId, string description)
        {
            this.data
                .Issues
                .Add(new Issue
                {
                    CarId = carId,
                    Description = description,
                    IsFixed = false
                });

            this.data.SaveChanges();
        }

        public void Delete(string issueId, string carId)
        {
            var issue = this.GetIssue(issueId, carId);

            if (issue != null)
            {
                this.data.Issues.Remove(issue);
                this.data.SaveChanges();
            }
        }

        public void Fix(string issueId, string carId)
        {
            var issue = this.GetIssue(issueId, carId);

            if (issue != null)
            {
                issue.IsFixed = true;
                data.SaveChanges();
            }
        }

        private Issue GetIssue(string issueId, string carId)
            => this.data
                .Issues
                .FirstOrDefault(i => i.Id == issueId && i.CarId == carId);
    }
}
