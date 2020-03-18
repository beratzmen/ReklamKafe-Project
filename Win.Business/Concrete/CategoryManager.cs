using System.Collections.Generic;
using Win.DataAccess.Abstract;
using Win.Entities.Model;

namespace Win.Business.Concrete
{
    public class CategoryManager
    {
        ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public void Add(Category entity)
        {
            _categoryDal.Add(entity);
        }

        public void Update(Category entity)
        {
            _categoryDal.Update(entity);
        }

        public void Delete(int Id)
        {
            _categoryDal.Delete(Id);
        }

        public Category Get(int Id)
        {
            return _categoryDal.Get(Id);
        }

        public List<Category> GetAll()
        {
            return _categoryDal.GetAll();
        }
    }
}
