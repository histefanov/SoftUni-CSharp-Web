namespace SharedTrip.Models.Trips
{
    using System;

    public class AddTripFormModel
    {
        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public string Date { get; set; }

        public string CarImage { get; set; }

        public int Seats { get; set; }

        public string Description { get; set; }
    }
}
