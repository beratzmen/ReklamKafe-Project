using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win.DataAccess.Abstract;
using Win.Entities.Enum;
using Win.Entities.Model;

namespace Win.DataAccess.Concrete.EntityFramework.Repository
{
    public class EfAdsManagementRepository : IAdsManagementDal
    {
        private Context context = new Context();

        public int Add(AdsManagement entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.AuditDate = DateTime.Now;
            entity.AuditStatus = (short)AuditStatus.created;
            context.AdsManagement.Add(entity);
            context.SaveChanges();
            return entity.Id;
        }
        public AdsManagement Get(int Id)
        {
            return context.AdsManagement.FirstOrDefault(x => x.AuditStatus != (short)AuditStatus.deleted && x.Id == Id);
        }
        public List<AdsManagement> GetAll()
        {
            return context.AdsManagement.Where(x => x.AuditStatus != (short)AuditStatus.deleted).ToList();
        }
        public AdsManagement GetByUserId(int userId)
        {
            return context.AdsManagement.FirstOrDefault(x => x.AuditStatus != (short)AuditStatus.deleted && x.UserId == userId);
        }
        public void Delete(int id)
        {
            var dbEntity = Get(id);
            dbEntity.AuditDate = DateTime.Now;
            dbEntity.AuditStatus = (short)AuditStatus.deleted;
            context.SaveChanges();
        }
        public bool Update(AdsManagement entity)
        {
            var item = Get(entity.Id);
            item.AuditDate = DateTime.Now;
            item.AuditStatus = (short)AuditStatus.updated;
            return (context.SaveChanges() > 0) ? true : false;
        }
    }
}
