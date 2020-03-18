using System.Collections.Generic;
using System.Linq;
using Win.DataAccess.Abstract;
using Win.Entities.Model;

namespace Win.DataAccess.Concrete.EntityFramework.Repository
{
    public class EfDistrictRepository : IDistrictDal
    {
        private Context context = new Context();

        public List<District> Get(int? id)
        {
            return context.District.Where(z => z.TownID == id).ToList();
        }

        public List<District> GetAll()
        {
            return context.District.ToList();
        }
    }
}
