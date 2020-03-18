using System.Collections.Generic;
using System.Linq;
using Win.DataAccess.Abstract;
using Win.Entities;
using Win.Entities.Model;

namespace Win.DataAccess.Concrete.EntityFramework.Repository
{
    public class EfTownRepository : ITownDal
    {
        private Context context = new Context();

        public List<Town> Get(int ?id)
        {
            return context.Town.Where(x => x.CityID == id).ToList();
        }

        public List<Town> GetAll()
        {
            return context.Town.ToList();
        }
    }
}
