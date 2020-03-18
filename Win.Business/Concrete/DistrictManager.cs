using System.Collections.Generic;
using Win.DataAccess.Abstract;
using Win.Entities.Model;

namespace Win.Business.Concrete
{
    public class DistrictManager
    {
        IDistrictDal _districtDal;
        public DistrictManager(IDistrictDal districtDal)
        {
            _districtDal = districtDal;
        }
        public List<District> GetAll()
        {
            return _districtDal.GetAll();

        }
        public List<District> Get(int? id)
        {
            return _districtDal.Get(id);
        }
    }
}
