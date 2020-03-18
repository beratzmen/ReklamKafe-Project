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
    public class EfContactRepository : IContactDal
    {
        private Context context = new Context();

        public void Add(Contact entity)
        {
            entity.Status = (short)ProcessStatus.pending;
            entity.AuditStatus = (short)AuditStatus.created;
            entity.AuditDate = DateTime.Now;
            entity.CreatedDate = DateTime.Now;
            context.Contact.Add(entity);
            context.SaveChanges();
        }

        public void Delete(int Id)
        {
            Contact contactUpdate = Get(Id);
            contactUpdate.AuditStatus = (short)AuditStatus.deleted;
            contactUpdate.AuditDate = DateTime.Now;
            context.SaveChanges();
        }

        public Contact Get(int Id)
        {
            return context.Contact.FirstOrDefault(x => x.AuditStatus != (short)AuditStatus.deleted && x.Id == Id);
        }

        public List<Contact> GetAll()
        {
            return context.Contact.Where(p => p.AuditStatus != (short)AuditStatus.deleted).ToList();
        }

        public void Update(Contact entity)
        {
            Contact contactUpdate = Get(entity.Id);
            contactUpdate.AuditStatus = (short)AuditStatus.updated;
            contactUpdate.AuditDate = DateTime.Now;
            context.SaveChanges();
        }
        public void ProcessCompleted(int id)
        {
            Contact contactUpdate = Get(id);
            contactUpdate.Status = (short)ProcessStatus.completed;
            Update(contactUpdate);
        }
    }
}
