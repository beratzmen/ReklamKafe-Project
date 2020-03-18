using System;
using System.Collections.Generic;
using Win.DataAccess.Abstract;
using Win.Entities.Model;

namespace Win.Business
{
    public class ProductManager
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public int Add(Product entity)
        {
            return _productDal.Add(entity);
        }

        public void Update(Product entity)
        {
            _productDal.Update(entity);
        }

        public void Delete(int Id)
        {
            _productDal.Delete(Id);
        }

        public Product Get(int Id)
        {
            return _productDal.Get(Id);
        }

        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }
        public List<Product> Search(string search)
        {
            return _productDal.Search(search);
        }

        public List<Product> GetLastAdded(int limit)
        {
            return _productDal.GetLastAdded(limit);
        }
        public List<Product> GetLastSolded(int limit)
        {
            return _productDal.GetLastSolded(limit);
        }
        public List<Product> GetProductsByActiveStatus(bool isActive)
        {
            return _productDal.GetProductsByActiveStatus(isActive);
        }
        public Tuple<List<Product>, List<Product>, List<Product>> GetMainPageSliderItems(int lastAddedLimit, bool isActive)
        {
            return _productDal.GetMainPageSliderItems(lastAddedLimit, isActive);
        }
        public List<Product> GetProductByFiltering(List<int> categoryIdList, float minPrice, float maxPrice, int sortingValue)
        {
            return _productDal.GetProductByFiltering(categoryIdList, minPrice, maxPrice, sortingValue);
        }
        public List<Product> GetProductByPrice(float minPrice, float maxPrice, int sortingValue)
        {
            return _productDal.GetProductByPrice(minPrice, maxPrice, sortingValue);
        }
        public void UpdateAmount(int productId, short amount)
        {
            _productDal.UpdateAmount(productId, amount);
        }
        public void DisplayCounter(int id)
        {
            _productDal.DisplayCounter(id);
        }
        public List<Product> TopProductDisplay()
        {
            return _productDal.TopProductDisplay();
        }
    }
}
