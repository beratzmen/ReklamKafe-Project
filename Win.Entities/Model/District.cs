using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win.Entities.Model
{
    [Table("District")]
    public partial class District
    {
        public int DistrictID { get; set; }
        public int TownID { get; set; }
        public string DistrictName { get; set; }
    }
}
