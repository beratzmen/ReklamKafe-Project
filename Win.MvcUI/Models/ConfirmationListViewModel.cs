using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Win.Entities.Model;

namespace Win.MvcUI.Models
{
    public class ConfirmationListViewModel
    {
        public Product Product { get; set; }
        public UserAdressInformation UserAdressInformation { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}