using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win.Entities.Model;

namespace Win.DataAccess.Abstract
{
    public interface IProductFavoriteDal
    {
        ProductFavorite Get(int id);
        ProductFavorite Get(int userId, int productId);
        int Add(ProductFavorite entity);
        void Delete(int id);
        List<Product> GetProductsByUserId(int userId);
        List<string> GetUsersByProductId(int productId);
    }
}
