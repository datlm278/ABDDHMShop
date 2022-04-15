using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_LTWeb.Session
{
    [Serializable]
    public class UserSession
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}