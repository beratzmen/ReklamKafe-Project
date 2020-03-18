using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win.DataAccess.Abstract;
using Win.Entities.Model;

namespace Win.Business.Concrete
{
    public class BulletinManager
    {
        IBulletinDal _bulletinDal;
        public BulletinManager(IBulletinDal bulletinDal)
        {
            _bulletinDal = bulletinDal;
        }
        public void Add(Bulletin entity)
        {
            _bulletinDal.Add(entity);
        }
    }
}
