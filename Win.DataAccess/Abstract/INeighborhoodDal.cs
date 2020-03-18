using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win.Entities.Model;

namespace Win.DataAccess.Abstract
{
    public interface INeighborhoodDal
    {
        List<Neighborhood> GetAll();
        List<Neighborhood> Get(int? id);
    }
}
