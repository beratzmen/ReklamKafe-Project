using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Win.MvcUI.Models
{
    public class ProductFilteringViewModel
    {
        public List<int> categoryList { get; set; }
        public float minPrice { get; set; }
        public float maxPrice { get; set; }
    }
}