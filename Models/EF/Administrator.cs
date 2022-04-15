namespace BTL_LTWeb.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Administrator")]
    public partial class Administrator
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        [StringLength(50)]
        public string name { get; set; }

        [StringLength(50)]
        public string username { get; set; }

        [StringLength(100)]
        public string password { get; set; }
    }
}
