using System;
using Win.DataAccess.Abstract;
using Win.Entities.Model;

namespace Win.DataAccess.Concrete.EntityFramework.Repository
{
    public class EfBulletinRepository : IBulletinDal
    {
        private Context context = new Context();
        public void Add(Bulletin entity)
        {
            entity.CreatedDate = DateTime.Now;
            context.Bulletin.Add(entity);
            context.SaveChanges();
        }
    }
}
