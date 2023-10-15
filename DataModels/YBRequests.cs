namespace YBCarRental3D_API.DataModels
{
    public class YBRequests
    {
    }

    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class PageRequest
    {
        public int PageSize { get; set; }
        public int PageNum { get; set; }
    }

}
