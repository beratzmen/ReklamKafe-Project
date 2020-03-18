using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Win.Entities.Model;

namespace Win.MvcUI.Models
{
    public class ProductAddViewModel
    {
        public Product product { get; set; }
        public int categoryId { get; set; }
    }
}