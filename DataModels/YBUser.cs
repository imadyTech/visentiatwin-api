

using Newtonsoft.Json;

namespace VisentiaTwin_API.DataModels
{

	public class YBUser : VTDataBasis
	{
		public YBUser() { }
		public YBUser(string username, int password) : this() { }
		~YBUser() { }

        [JsonProperty] public string UserName { get; set; }                                     //max 12 alphabets; No verification in this application.
        [JsonProperty] public string FirstName { get; set; }
        [JsonProperty] public string FamilyName { get; set; }
        [JsonProperty] public string Password { get; set; }                                     //max 6 digits alphabet/numerics;
        [JsonProperty] public string UserRoles { get; set; }                                    //multiple roles are allowed, separated by "|"
        [JsonProperty] public bool LoginStatus { get; set; }                                    //true: logged in; false: logged out;
        [JsonProperty] public double Balance { get; set; }                                      //Account Balance allows user to hire a car (fail to rent if no sufficient balance).
    };

}
