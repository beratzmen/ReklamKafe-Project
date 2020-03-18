using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win.DataAccess.Abstract;
using Win.Entities.Model;

namespace Win.Business.Concrete
{
    public class NeighborhoodManager
    {
        INeighborhoodDal _neighborhoodDal;
        public NeighborhoodManager(INeighborhoodDal neighborhoodDal)
        {
            _neighborhoodDal = neighborhoodDal;
        }
        public List<Neighborhood> GetAll()
        {
            return _neighborhoodDal.GetAll();

        }
        public List<Neighborhood> Get(int? id)
        {
            return _neighborhoodDal.Get(id);
        }
    }
}
