using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTL_LTWeb.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username cannot be empty")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}