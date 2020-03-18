using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win.DataAccess.Abstract;
using Win.Entities.Model;

namespace Win.Business.Concrete
{
    public class CityManager
    {
        ICityDal _cityDal;
        public CityManager(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }
        public List<City> GetAll()
        {
            return _cityDal.GetAll();
        }
    }
}
