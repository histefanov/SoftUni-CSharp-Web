namespace SharedTrip.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SharedTrip.Data;
    using SharedTrip.Models;
    using SharedTrip.Models.Trips;
    using SharedTrip.Services;
    using System;
    using System.Globalization;
    using System.Linq;

    public class TripsController : Controller
    {
        private readonly IValidator validator;
        private readonly ApplicationDbContext dbContext;

        public TripsController(IValidator validator, ApplicationDbContext dbContext)
        {
            this.validator = validator;
            this.dbContext = dbContext;
        }

        [Authorize]
        public HttpResponse Add() => View();

        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddTripFormModel model)
        {
            var modelErrors = this.validator.ValidateTripCreation(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var trip = new Trip
            {
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                DepartureTime = DateTime.Parse(model.DepartureTime),
                Seats = model.Seats,
                Description = model.Description,
                ImagePath = model.ImagePath,
            };

            this.dbContext.Trips.Add(trip);
            dbContext.SaveChanges();

            return Redirect("/Trips/All");
        }

        [Authorize]
        public HttpResponse All()
        {
            var trips = this.dbContext.Trips
                .Select(t => new TripListingViewModel
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = 
                        t.DepartureTime.ToString("dd.MM.yyyy") + 
                        " г. " + 
                        t.DepartureTime.ToString("HH:mm:ss"),
                    Seats = t.Seats,
                })
                .ToList();

            return View(trips);
        }

        [Authorize]
        public HttpResponse Details()
        {
            var tripId = this.Request.Query["tripId"];

            var tripData = this.dbContext.Trips
                .Where(t => t.Id == tripId)
                .Select(t => new TripDetailsViewModel
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    Seats = t.Seats,
                    Description = t.Description,
                    ImagePath = t.ImagePath
                })
                .FirstOrDefault();
            
            return View(tripData);
        }

        [Authorize]
        public HttpResponse AddUserToTrip()
        {
            var tripId = this.Request.Query["tripId"];
            var trip = this.dbContext.Trips.FirstOrDefault(x => x.Id == tripId);

            if (this.dbContext.UserTrips.Any(x => x.Trip == trip))
            {
                return Redirect($"/Trips/Details?tripId={trip.Id}");
            }

            trip.UserTrips.Add(new UserTrip
            {
                TripId = tripId,
                UserId = this.User.Id
            });

            trip.Seats--;

            this.dbContext.SaveChanges();

            return Redirect("/Trips/All");
        }
    }
}
