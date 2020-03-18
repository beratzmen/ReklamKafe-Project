using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Win.AspMvcUI.Models
{
    public class ProductFilteringViewModel
    {
        public int categoryId { get; set; }
        public float minPrice { get; set; }
        public float maxPrice { get; set; }
        public int sortingValue { get; set; }
    }
}