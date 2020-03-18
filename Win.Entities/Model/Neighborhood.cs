using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win.Entities.Model
{
    [Table("Neighborhood")]
    public class Neighborhood
    {
        public int NeighborhoodID { get; set; }
        public int DistrictID { get; set; }
        public string NeighborhoodName { get; set; }
    }
}
