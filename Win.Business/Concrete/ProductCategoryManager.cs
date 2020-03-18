using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win.DataAccess.Abstract;
using Win.Entities.Model;

namespace Win.Business.Concrete
{
    public class ProductCategoryManager
    {
        IProductCategoryDal _productCategoryDal;
        public ProductCategoryManager(IProductCategoryDal categoryDal)
        {
            _productCategoryDal = categoryDal;
        }
        public void Add(ProductCategory entity)
        {
            _productCategoryDal.Add(entity);
        }

        public void Update(ProductCategory entity)
        {
            _productCategoryDal.Update(entity);
        }

        public ProductCategory Get(int Id)
        {
            return _productCategoryDal.Get(Id);
        }
    }
}