using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_LTWeb.Areas.Admin.Data
{
    [Serializable]
    public class AdminSession
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
    }
}