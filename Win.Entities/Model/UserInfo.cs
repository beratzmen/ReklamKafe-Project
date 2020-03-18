namespace Win.Entities.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Win.Helpers.Util;

    [Table("UserInfo")]
    public partial class UserInfo
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string NickName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public double Point { get; set; }

        public string PhotoUrl { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime AuditDate { get; set; }

        public short AuditStatus { get; set; }
        [NotMapped]
        public string Title
        {
            get
            {
                return this.Title = Calculation.GetTitle(this.Point);
            }
            set { }
        }
    }
}
