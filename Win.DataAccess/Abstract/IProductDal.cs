using System;
using System.Collections.Generic;
using Win.Entities.Model;

namespace Win.DataAccess.Abstract
{
    public interface IProductDal
    {
        List<Product> GetAll();
        Product Get(int Id);
        int Add(Product entity);
        void Delete(int Id);
        void Update(Product entity);
        List<Product> GetLastAdded(int limit);
        List<Product> Search(string search);
        List<Product> GetLastSolded(int limit);
        List<Product> GetProductsByActiveStatus(bool isActive);
        Tuple<List<Product>, List<Product>, List<Product>> GetMainPageSliderItems(int lastAddedLimit, bool isActive);
        List<Product> GetProductByFiltering(List<int> categoryIdList, float minPrice, float maxPrice, int sortingValue);
        List<Product> GetProductByPrice(float minPrice, float maxPrice, int sortingValue);
        void UpdateAmount(int productId, short amount);
        void DisplayCounter(int id);
        List<Product> TopProductDisplay();
    }
}
