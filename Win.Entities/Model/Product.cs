namespace Win.Entities.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public double Price { get; set; }

        public long? DisplayCount { get; set; }

        public string Upload1 { get; set; }

        public string Upload2 { get; set; }

        public string Upload3 { get; set; }

        public bool IsActive { get; set; }

        public DateTime SalesDate { get; set; }

        public DateTime DateOfSold { get; set; }

        public short Amount { get; set; }

        public DateTime AuditDate { get; set; }

        public short AuditStatus { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
