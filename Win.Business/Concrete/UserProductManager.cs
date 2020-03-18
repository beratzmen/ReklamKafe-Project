using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win.DataAccess.Abstract;
using Win.Entities.Dto;
using Win.Entities.Model;

namespace Win.Business.Concrete
{
    public class UserProductManager
    {
        IUserProductDal _userProductDal;
        public UserProductManager(IUserProductDal userProductDal)
        {
            _userProductDal = userProductDal;
        }

        public List<UserProductViewModel> GetUserProduct()
        {
            return _userProductDal.GetUserProduct();
        }
        public UserProductViewModel GetUserProduct(int id)
        {
            return _userProductDal.GetUserProduct(id);
        }
        public UserProductViewModel GetUserByProductId(int id, int userId)
        {
            return _userProductDal.GetUserByProductId(id, userId);
        }
        public UserProduct Get(int id)
        {
            return _userProductDal.Get(id);
        }
        public List<UserProduct> GetAll()
        {
            return _userProductDal.GetAll();
        }
        public List<UserProduct> GetAll(int userId)
        {
            return _userProductDal.GetAll(userId);
        }

        public int Add(UserProduct entity)
        {
            return _userProductDal.Add(entity);
        }
        public void Update(UserProduct entity)
        {
            _userProductDal.Update(entity);
        }
        public void Delete(int id)
        {
            _userProductDal.Delete(id);
        }
        public void ProcessCompleted(int id)
        {
            _userProductDal.ProcessCompleted(id);
        }
        public List<UserProductViewModel> GetProductsByUserId(int userId)
        {
            return _userProductDal.GetProductsByUserId(userId);
        }
        public List<string> GetUsersByProductId(int productId)
        {
            return _userProductDal.GetUsersByProductId(productId);
        }
    }
}
