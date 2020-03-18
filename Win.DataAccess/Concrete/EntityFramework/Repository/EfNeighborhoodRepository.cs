using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win.DataAccess.Abstract;
using Win.Entities.Model;

namespace Win.DataAccess.Concrete.EntityFramework.Repository
{
    public class EfNeighborhoodRepository : INeighborhoodDal
    {
        private Context context = new Context();

        public List<Neighborhood> Get(int? id)
        {
            return context.Neighborhood.Where(z => z.DistrictID == id).ToList();
        }

        public List<Neighborhood> GetAll()
        {
            return context.Neighborhood.ToList();
        }
    }
}
