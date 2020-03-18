using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win.Entities.Model;

namespace Win.Entities.Dto
{
    public class UserAdressInformationViewModel
    {
        public UserInfo userInfo { get; set; }
        public UserAdressInformation adressInformatin { get; set; }
        public List<City> cities { get; set; }
        public int? cityId { get; set; }
        public string city { get; set; }
        public List<Town> towns { get; set; }
        public int? townId { get; set; }
        public string town { get; set; }
        public List<District> districts { get; set; }
        public int? districtId { get; set; }
        public string district { get; set; }
        public List<Neighborhood> neighborhoods { get; set; }
        public int? neighborhoodId { get; set; }
        public string neighborhood { get; set; }
        public List<Product> myFavoriteProducts { get; set; }
        public List<UserProductViewModel> product { get; set; }
        public List<UserProduct> usersProducts { get; set; }
    }
}
