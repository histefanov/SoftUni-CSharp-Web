﻿namespace SharedTrip.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SharedTrip.Data;
    using SharedTrip.Models;
    using SharedTrip.Models.Trips;
    using SharedTrip.Services;
    using System;
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
                DepartureTime = DateTime.Parse(model.Date),
                Seats = model.Seats,
                Description = model.Description,
                ImagePath = model.CarImage,
            };

            this.dbContext.Trips.Add(trip);
            dbContext.SaveChanges();

            return Redirect("/Trips/All");
        }

        public HttpResponse All()
        {       
            return View();
        }
    }
}