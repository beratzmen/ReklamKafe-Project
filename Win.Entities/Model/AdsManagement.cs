namespace Win.Entities.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdsManagement")]
    public partial class AdsManagement
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LegalClick { get; set; }
        public int IllegalClick { get; set; }
        public int TotalClick { get; set; }
        public DateTime AuditDate { get; set; }
        public short AuditStatus { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
