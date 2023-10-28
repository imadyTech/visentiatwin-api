namespace YBCarRental3D_API.DataModels
{
    public class YBCar
    {
        public int      Id { get; set; }
        public string?  Make { get; set; }
        public string?  Model { get; set; }
        public string?  UnityModelName { get; set; }
        public int      Year { get; set; }
        public int      Mileage { get; set; }
        public bool     IsAvailable { get; set; }//is the car available now?
        public int      MinRentPeriod { get; set; }//day
        public int      MaxRentPeriod { get; set; }//day
        public double   DayRentPrice { get; set; }//rental price per day;

    }
}
