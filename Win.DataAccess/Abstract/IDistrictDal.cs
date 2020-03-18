using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win.Entities.Model;

namespace Win.DataAccess.Abstract
{
    public interface IDistrictDal
    {
        List<District> GetAll();
        List<District> Get(int? id);
    }
}
