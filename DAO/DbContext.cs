using BTL_LTWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_LTWeb.DAO
{
    public class DbContext
    {
        private static WebBanHangDbContext dbContext = null;

        public static WebBanHangDbContext GetDbContext()
        {
            if (dbContext == null)
            {
                dbContext = new WebBanHangDbContext();
                return dbContext;
            }
            return dbContext;
        }
    }
}