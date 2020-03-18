using System.Collections.Generic;
using Win.Entities.Model;

namespace Win.DataAccess.Abstract
{
    public interface IUserInfoDal
    {
        List<UserInfo> GetAll();
        UserInfo Get(int Id);
        bool Add(UserInfo entity);
        void Delete(int Id);
        bool Update(UserInfo entity);
        UserInfo UserLogin(UserInfo entity);
        void UpdatePoint(int userId, double point);
        List<UserInfo> GetStandings();
        UserInfo GetByEmail(string email);
        void ResetPassword(UserInfo entity);
        List<UserInfo> TopUsers(int limit);
    }
}
