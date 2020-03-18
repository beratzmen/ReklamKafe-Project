using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Win.Entities.Dto;
using Win.Entities.Model;

namespace Win.AspMvcUI.Models
{
    public class ConfirmationListViewModel
    {
        public Product Product { get; set; }
        public UserAdressInformation UserAdressInformation { get; set; }
        public UserAdressInformationViewModel UserAdress { get; set; }
        public UserProductViewModel UserProductViewModel { get; set; }
        public UserInfo UserInfo { get; set; }

    }
}