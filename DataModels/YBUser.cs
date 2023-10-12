namespace YBCarRental3D_API.DataModels
{
    public class YBUser
    {
        public int      Id { get; set; }
        public string   UserName{get;set;}                  //max 12 alphabets; No verification in this application.
        public string   FirstName{get;set;}
        public string   FamilyName{get;set;}
        public string   Password{get;set;}                  //max 6 digits alphabet/numerics;
        public string   UserRoles{get;set;}                 //multiple roles are allowed, separated by "|"
        public bool     LoginStatus{get;set;}               //true: logged in; false: logged out;
        public double   Balance { get; set; }				//allows user to hire a car (fail to rent if no sufficient balance).
    }
}
