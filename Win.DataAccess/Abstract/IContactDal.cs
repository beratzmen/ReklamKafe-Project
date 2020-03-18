using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win.Entities.Model;

namespace Win.DataAccess.Abstract
{
    public interface IContactDal
    {
        List<Contact> GetAll();
        Contact Get(int Id);
        void Add(Contact entity);
        void Delete(int Id);
        void Update(Contact entity);
        void ProcessCompleted(int id);
    }
}
