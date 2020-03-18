using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win.DataAccess.Abstract;
using Win.Entities.Model;

namespace Win.Business.Concrete
{
    public class ContactManager
    {
        IContactDal _contactDal;
        public ContactManager(IContactDal contactDal)
        {
            _contactDal = contactDal;
        }
        public void Add(Contact entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Message))
                entity.Message = "Birden fazla boşluk girilmiş.";
            _contactDal.Add(entity);
        }

        public void Update(Contact entity)
        {
            _contactDal.Update(entity);
        }

        public void Delete(int id)
        {
            _contactDal.Delete(id);
        }

        public Contact Get(int id)
        {
            return _contactDal.Get(id);
        }

        public List<Contact> GetAll()
        {
            return _contactDal.GetAll();
        }
        public void ProcessCompleted(int id)
        {
            _contactDal.ProcessCompleted(id);
        }
    }
}
