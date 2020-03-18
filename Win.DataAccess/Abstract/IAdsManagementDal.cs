using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win.Entities.Model;

namespace Win.DataAccess.Abstract
{
    public interface IAdsManagementDal
    {
        AdsManagement Get(int id);
        AdsManagement GetByUserId(int userId);
        List<AdsManagement> GetAll();
        int Add(AdsManagement entity);
        void Delete(int id);
        bool Update(AdsManagement entity);
    }
}
