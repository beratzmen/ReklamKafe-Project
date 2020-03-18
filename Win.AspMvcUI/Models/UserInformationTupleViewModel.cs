using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Win.Entities.Dto;
using Win.Entities.Model;

namespace Win.AspMvcUI.Models
{
    public class UserInformationTupleViewModel
    {
        public UserInfo userInfo { get; set; }
        public UserAdressInformation adressInformatin { get; set; }

        //Entities katmanında dto'nun altında join ile kullanıcıya ait city,town,district bilgisini alıyorum buraya
        public UserAdressInformationViewModel adressInformatinViewModel { get; set; }
        public List<Product> product { get; set; }
        public List<City> cities { get; set; }
        public List<Town> towns { get; set; }
        public List<District> districts { get; set; }

    }
}