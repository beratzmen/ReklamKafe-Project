namespace Win.Entities.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserProduct")]
    public partial class UserProduct
    {
        public int Id { get; set; }

        public int UserID { get; set; }

        public int ProductID { get; set; }

        public short Status { get; set; }

        public short AuditStatus { get; set; }

        public DateTime AuditDate { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
