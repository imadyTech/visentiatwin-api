
using Newtonsoft.Json;
using System.Threading;

namespace VisentiaTwin_API.DataModels
{
	public class YBCar : VTDataBasis
	{
		public YBCar()
		{
			Id = -1;
			Make = "";
			Model = "";
			Year = -1;
			Mileage = -1;
			IsAvailable = true;
			MinRentPeriod = 1;
			MaxRentPeriod = 30;
			DayRentPrice = 0;

			persistentSeparator = ';';
		}
		public YBCar(string defString):this()
		{
			base.serializedString = defString;
            Deserialize(defString);
		}
		~YBCar() { }

		[JsonProperty] public string	Make{ get; set; }
		[JsonProperty] public string	Model { get; set; }
        [JsonProperty] public int		Year { get; set; }
        [JsonProperty] public int		Mileage { get; set; }
        [JsonProperty] public bool		IsAvailable { get; set; }                   //is the car available now?
        [JsonProperty] public int		MinRentPeriod { get; set; }                 //day
        [JsonProperty] public int		MaxRentPeriod { get; set; }                 //day
        [JsonProperty] public double	DayRentPrice { get; set; }                  //rental price per day;
        [JsonProperty] public string?	UnityModelName { get; set; }				//indicating the gameobject name of the 3D model

        public void Deserialize(string line)
        {
            base.SplitLine(line);

            if (base.HasValue("Id"))				base.Id = int.Parse(FindValue("Id"));
            if (base.HasValue("Make"))				Make= FindValue("Make");
            if (base.HasValue("Model"))				Model= FindValue("Model");
            if (base.HasValue("Year"))				Year = int.Parse(FindValue("Year"));
            if (base.HasValue("Mileage"))			Mileage = int.Parse(FindValue("Mileage"));
            if (base.HasValue("IsAvailable"))		IsAvailable= FindValue("IsAvailable")=="1";
            if (base.HasValue("MinRentPeriod"))		MinRentPeriod= int.Parse(FindValue("MinRentPeriod"));
            if (base.HasValue("MaxRentPeriod"))		MaxRentPeriod= int.Parse(FindValue("MaxRentPeriod"));
            if (base.HasValue("DayRentPrice"))		DayRentPrice= float.Parse(FindValue("DayRentPrice"));
            if (base.HasValue("UnityModelName"))	UnityModelName = FindValue("UnityModelName");

        }

    };
}
