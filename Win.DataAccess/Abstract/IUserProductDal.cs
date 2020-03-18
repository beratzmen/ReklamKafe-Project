using System.Collections.Generic;
using Win.Entities.Dto;
using Win.Entities.Model;

namespace Win.DataAccess.Abstract
{
    public interface IUserProductDal
    {
        List<UserProductViewModel> GetUserProduct();
        UserProductViewModel GetUserProduct(int id);
        UserProductViewModel GetUserByProductId(int id, int userId);


        UserProduct Get(int id);
        List<UserProduct> GetAll();
        List<UserProduct> GetAll(int userId);
        int Add(UserProduct entity);
        void Update(UserProduct entity);
        void Delete(int id);
        List<UserProductViewModel> GetProductsByUserId(int userId);
        List<string> GetUsersByProductId(int productId);
        void ProcessCompleted(int id);
    }
}
