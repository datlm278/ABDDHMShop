using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTL_LTWeb.Models
{
    public class LoginModel
    {
        [Key]
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username can not be empty")]
        public string userName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password can not be empty")]
        public string password { get; set; }
    }
}