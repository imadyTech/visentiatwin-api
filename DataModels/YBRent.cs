namespace YBCarRental3D_API.DataModels
{
    public class YBRent
    {
        public int         Id { get; set; }
        public int         UserId{get;set;}
        public int         CarId{get;set;}
        public DateTime    RentStart{get;set;}                       //rental start date
        public DateTime    DateOfOrder{get;set;}                 //date placed order
        public int         RentDays{get;set;}                       //total days of rental
        public string      Status { get; set; }							//check YB_Global_Header for definition

    }
}
