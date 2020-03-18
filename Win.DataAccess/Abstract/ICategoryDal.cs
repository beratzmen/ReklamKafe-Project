using System.Collections.Generic;
using Win.Entities.Model;

namespace Win.DataAccess.Abstract
{
    public interface ICategoryDal
    {
        List<Category> GetAll();
        Category Get(int Id);
        void Add(Category entity);
        void Delete(int Id);
        void Update(Category entity);
    }
}
