using System.Collections.Generic;
using System.Linq;
using Win.DataAccess.Abstract;
using Win.Entities.Model;

namespace Win.DataAccess.Concrete.EntityFramework.Repository
{
    public class EfCityRepository : ICityDal
    {
        private Context context = new Context();
        public List<City> GetAll()
        {
            return context.City.ToList();
        }
    }
}
