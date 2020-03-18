using System.Collections.Generic;
using Win.DataAccess.Abstract;
using Win.Entities.Model;
using Win.Helpers.Util;

namespace Win.Business.Concrete
{
    public class UserInfoManager
    {
        IUserInfoDal _userInfoDal;
        public UserInfoManager(IUserInfoDal userInfoDal)
        {
            _userInfoDal = userInfoDal;
        }
        public bool Add(UserInfo entity)
        {
            entity.Password = Encrypt.Md5(entity.Password);
            return _userInfoDal.Add(entity);
        }

        public bool Update(UserInfo entity)
        {
            if (string.IsNullOrEmpty(entity.Password) || string.IsNullOrWhiteSpace(entity.Password))
                return false;
            var dbUserInfo = Get(entity.Id);
            if (dbUserInfo == null)
                return false;
            if (entity.Password == "********")
                entity.Password = Encrypt.Md5Decrypt(dbUserInfo.Password);
            entity.Password = Encrypt.Md5(entity.Password);
            return _userInfoDal.Update(entity);
        }

        public void Delete(int Id)
        {
            _userInfoDal.Delete(Id);
        }

        public UserInfo Get(int Id)
        {
            return _userInfoDal.Get(Id);
        }

        public List<UserInfo> GetAll()
        {
            return _userInfoDal.GetAll();
        }

        public UserInfo UserLogin(UserInfo entity)
        {
            if (string.IsNullOrEmpty(entity.NickName) || string.IsNullOrEmpty(entity.Password))
                return null;
            entity.Password = Encrypt.Md5(entity.Password);
            var user = _userInfoDal.UserLogin(entity);
            if (user == null)
                return null;
            else
                return user;
        }
        public void UpdatePoint(int userId, double point)
        {
            _userInfoDal.UpdatePoint(userId, point);
        }
        public List<UserInfo> GetStandings()
        {
            return _userInfoDal.GetStandings();
        }
        public UserInfo GetByEmail(string email)
        {
            return _userInfoDal.GetByEmail(email);
        }
        public void ResetPassword(string email)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
                return;
            if (!Email.IsValidEmail(email))
                return;
            var user = GetByEmail(email);
            if (user == null)
                return;
            string newPassword = Encrypt.CreateRandomPassword();
            user.Password = Encrypt.Md5(newPassword);
            _userInfoDal.ResetPassword(user);
            Email.SendMail("destek@reklamkafe.com", "AliBerat41", user.Email, "Reklam Kafe | Şifre Yenileme İsteği", "Merhaba " + user.NickName + "\n\nİsteğiniz üzerine yeni şifrenizi gönderdik. www.reklamkafe.com adresine giriş yaparak ücretsiz ürünlerinizi almaya devam edebilirsiniz.\nYeni Şifreniz: " + newPassword + "\n\n\nReklam Kafe Ailesi\nwww.reklamkafe.com");
        }
        public List<UserInfo> TopUsers(int limit)
        {
            return _userInfoDal.TopUsers(limit);
        }
    }
}
