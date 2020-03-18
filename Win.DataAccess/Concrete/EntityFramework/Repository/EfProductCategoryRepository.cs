using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Win.DataAccess.Abstract;
using Win.Entities.Enum;
using Win.Entities.Model;

namespace Win.DataAccess.Concrete.EntityFramework.Repository
{
    public class EfProductCategoryRepository : IProductCategoryDal
    {
        private Entities.Model.Context context = new Entities.Model.Context();

        public void Add(ProductCategory entity)
        {
            context.ProductCategory.Add(entity);
            context.SaveChanges();
        }

        public ProductCategory Get(int Id)
        {
            return context.ProductCategory.FirstOrDefault(x => x.Id == Id);
        }

        public void Update(ProductCategory entity)
        {
            ProductCategory categoryUpdate = Get(entity.Id);
            categoryUpdate.CategoryID = entity.CategoryID;
            categoryUpdate.ProductID = entity.ProductID;
            context.SaveChanges();
        }
        public ProductCategory GetByItemId(int productId, int categoryId)
        {
            return context.ProductCategory.FirstOrDefault(x => x.ProductID == productId && x.CategoryID == categoryId);
        }
    }
}
