using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win.DataAccess.Abstract;
using Win.Entities.Dto;
using Win.Entities.Model;

namespace Win.Business.Concrete
{
    public class ProductFavoriteManager
    {
        IProductFavoriteDal _productFavoriteDal;
        public ProductFavoriteManager(IProductFavoriteDal productFavoriteDal)
        {
            _productFavoriteDal = productFavoriteDal;
        }
        public ProductFavorite Get(int id)
        {
            return _productFavoriteDal.Get(id);
        }
        public ProductFavorite Get(int userId, int productId)
        {
            return _productFavoriteDal.Get(userId, productId);
        }
        public int Add(ProductFavorite entity)
        {
            return _productFavoriteDal.Add(entity);
        }
        public void Delete(int id)
        {
            _productFavoriteDal.Delete(id);
        }
        public List<Product> GetProductsByUserId(int userId)
        {
            return _productFavoriteDal.GetProductsByUserId(userId);
        }
        public List<string> GetUsersByProductId(int productId)
        {
            return _productFavoriteDal.GetUsersByProductId(productId);
        }
    }
}
