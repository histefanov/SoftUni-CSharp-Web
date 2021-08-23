namespace CarShop.Services.Issues
{
    using CarShop.Models.Cars;

    public interface IIssueService
    {
        CarIssuesViewModel All(string carId);

        void Add(string carId, string description);

        void Delete(string issueId, string carId);

        void Fix(string issueId, string carId);
    }
}
