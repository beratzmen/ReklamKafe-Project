namespace Win.Entities.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("rkdb.Bulletin")]
    public partial class Bulletin
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Mail { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
