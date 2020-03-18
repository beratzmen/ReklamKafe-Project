using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win.Entities.Model;

namespace Win.DataAccess.Abstract
{
    public interface IProductCategoryDal
    {
        void Add(ProductCategory entity);
        ProductCategory Get(int id);
        void Update(ProductCategory entity);
        ProductCategory GetByItemId(int productId, int categoryId);
    }
}