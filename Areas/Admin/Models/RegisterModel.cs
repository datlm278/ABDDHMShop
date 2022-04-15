using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTL_LTWeb.Areas.Admin.Models
{
    public class RegisterModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name can not be empty")]
        public string name { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username can not be empty")]
        public string username { get; set; }
        [Display(Name = "Password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must have at least 6 characters")]
        [Required(ErrorMessage = "Password can not be empty")]
        public string password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("password", ErrorMessage = "Password incorrect")]
        public string confirmPassword { get; set; }
        
    }
}