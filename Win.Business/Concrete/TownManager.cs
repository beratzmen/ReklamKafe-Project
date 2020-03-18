using System.Collections.Generic;
using Win.DataAccess.Abstract;
using Win.Entities.Model;

namespace Win.Business.Concrete
{
    public class TownManager
    {
        ITownDal _townDal;
        public TownManager(ITownDal townDal)
        {
            _townDal = townDal;
        }
        public List<Town> GetAll()
        {
            return _townDal.GetAll();
        }
        public List<Town> Get(int ?id)
        {
            return _townDal.Get(id);
        }
    }
}
