using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Win.Entities.Model;

namespace Win.AspMvcUI.Models
{
    public class MainPageViewModel
    {
        public Tuple<List<Product>, List<Product>, List<Product>> products { get; set; }
        public List<UserInfo> users { get; set; }
    }
}