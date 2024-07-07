using System.ComponentModel;

namespace VisentiaTwin_API.DataModels
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
        [DefaultValue (10)]
        public int PageSize { get; set; }

        [DefaultValue (0)]
        public int PageNum { get; set; }
    }

}
