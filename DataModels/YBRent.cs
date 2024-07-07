using Newtonsoft.Json;
using System;

namespace VisentiaTwin_API.DataModels
{

    /// <summary>
    /// The record of a rental order;
    /// </summary>
    public class YBRent : VTDataBasis
    {
        public const string YB_Rental_Status_Pending   = "pending";
        public const string YB_Rental_Status_Approved  = "approved";
        public const string YB_Rental_Status_Rejected  = "rejected";
        public const string YB_Rental_Status_Completed = "completed";

        public YBRent() { }
        public YBRent(YBUser user, YBCar car, DateTime start, int days) : this() { }
        public YBRent(int userId, int carId, DateTime start, int days) : this() { }
        ~YBRent() { }

        [JsonProperty] public int UserId { get; set; }
        [JsonProperty] public int CarId { get; set; }
        [JsonProperty] public DateTime RentStart { get; set; }                      //rental start date
        [JsonProperty] public DateTime DateOfOrder { get; set; }                    //date placed order
        [JsonProperty] public int RentDays { get; set; }                            //total days of rental
        [JsonProperty] public string Status { get; set; }                           //check YB_Global_Header for definition

    };
}

