using System.Collections.Generic;
using System.Linq;
using Win.DataAccess.Abstract;
using Win.Entities.Enum;
using Win.Entities.Model;

namespace Win.DataAccess.Concrete.EntityFramework.Repository
{
    public class EfCategoryRepository : ICategoryDal
    {
        private Context context = new Context();

        public void Add(Category entity)
        {
            context.Category.Add(entity);
            context.SaveChanges();
        }

        public void Delete(int Id)
        {
            Category categoryUpdate = Get(Id);
            categoryUpdate.AuditStatus = (short)AuditStatus.deleted;
            context.SaveChanges();
        }

        public Category Get(int Id)
        {
            return context.Category.FirstOrDefault(x => x.AuditStatus != (short)AuditStatus.deleted && x.Id == Id);
        }

        public List<Category> GetAll()
        {
            return context.Category.Where(p => p.AuditStatus != (short)AuditStatus.deleted).ToList();
        }

        public void Update(Category entity)
        {
            Category categoryUpdate = Get(entity.Id);
            categoryUpdate.Name = entity.Name;
            categoryUpdate.AuditStatus = (short)AuditStatus.updated;
            context.SaveChanges();
        }
    }
}
