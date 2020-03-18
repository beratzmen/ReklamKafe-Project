namespace Win.Entities.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserAdressInformation")]
    public partial class UserAdressInformation
    {
        public int Id { get; set; }

        public int UserID { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(150)]
        public string Surname { get; set; }

        public string Adress { get; set; }

        public int? CityId { get; set; }

        public int? TownId { get; set; }

        public int? DistrictId { get; set; }

        public int? NeighborhoodId { get; set; }

        [StringLength(11)]
        public string Phone { get; set; }

        public string Note { get; set; }

        public DateTime AuditDate { get; set; }

        public short AuditStatus { get; set; }

        public DateTime CreatedDate { get; set; }

        //[NotMapped]
        //public string CityName { get; set; }
    }
}
