using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win.Entities.Model
{
    [Table("City")]
    public partial class City
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
    }
}
