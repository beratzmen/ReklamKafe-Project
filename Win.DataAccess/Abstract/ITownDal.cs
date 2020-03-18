using System.Collections.Generic;
using Win.Entities.Model;

namespace Win.DataAccess.Abstract
{
    public interface ITownDal
    {
        List<Town> GetAll();
        List<Town> Get(int ?id);
    }
}
