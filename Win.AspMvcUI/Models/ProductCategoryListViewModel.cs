using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Win.Entities.Model;

namespace Win.AspMvcUI.Models
{
    public class ProductCategoryListViewModel
    {
        public List<Product> Product = new List<Product>();
        public List<Product> SearchProduct = new List<Product>();
        public List<Category> Category = new List<Category>();
    }
}