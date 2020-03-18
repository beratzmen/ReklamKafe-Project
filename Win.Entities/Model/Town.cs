using System.ComponentModel.DataAnnotations.Schema;

namespace Win.Entities.Model
{
    [Table("Town")]
    public partial class Town
    {
        public int TownID { get; set; }
        public int CityID { get; set; }
        public string TownName { get; set; }
    }
}
