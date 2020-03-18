using System;
using System.Collections.Generic;
using System.Linq;
using Win.DataAccess.Abstract;
using Win.Entities.Enum;
using Win.Entities.Model;

namespace Win.DataAccess.Concrete.EntityFramework.Repository
{
    public class EfUserInfoRepository : IUserInfoDal
    {
        private Context context = new Context();

        public bool Add(UserInfo entity)
        {
            entity.AuditStatus = (short)AuditStatus.created;
            entity.CreatedDate = DateTime.Now;
            entity.AuditDate = DateTime.Now;
            var existUser = context.UserInfo.FirstOrDefault(x => x.AuditStatus != (short)AuditStatus.deleted && x.NickName == entity.NickName);
            if (existUser == null)
            {
                context.UserInfo.Add(entity);
                context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void Delete(int Id)
        {
            UserInfo uiUpdate = Get(Id);
            uiUpdate.AuditDate = DateTime.Now;
            uiUpdate.AuditStatus = (short)AuditStatus.deleted;
            context.SaveChanges();
        }

        public UserInfo Get(int Id)
        {
            var user = context.UserInfo.FirstOrDefault(x => x.AuditStatus != (short)AuditStatus.deleted && x.Id == Id);
            if (user == null)
                return null;
            else
                return user;
        }

        public List<UserInfo> GetAll()
        {
            return context.UserInfo.Where(p => p.AuditStatus != (short)AuditStatus.deleted).ToList();
        }

        public bool Update(UserInfo entity)
        {
            UserInfo uiUpdate = Get(entity.Id);
            uiUpdate.Password = entity.Password;
            uiUpdate.PhotoUrl = entity.PhotoUrl;
            uiUpdate.Email = entity.Email;
            uiUpdate.AuditDate = DateTime.Now;
            uiUpdate.AuditStatus = (short)AuditStatus.updated;
            return (context.SaveChanges() > 0) ? true : false;
        }
        public UserInfo UserLogin(UserInfo entity)
        {
            return context.UserInfo.FirstOrDefault(p => p.NickName == entity.NickName && p.Password == entity.Password);
        }
        public void UpdatePoint(int userId, double point)
        {
            UserInfo uiUpdate = Get(userId);
            uiUpdate.Point -= point;
            Update(uiUpdate);
        }
        public List<UserInfo> GetStandings()
        {
            return context.UserInfo.Where(p => p.AuditStatus != (short)AuditStatus.deleted && !p.IsAdmin).OrderByDescending(p => p.Point).ToList();
        }
        public UserInfo GetByEmail(string email)
        {
            return context.UserInfo.FirstOrDefault(x => x.AuditStatus != (short)AuditStatus.deleted && x.Email == email);
        }
        public void ResetPassword(UserInfo entitiy)
        {
            Update(entitiy);
        }
        public List<UserInfo> TopUsers(int limit)
        {
            var list = context.UserInfo.Where(p => p.AuditStatus != (short)AuditStatus.deleted && !p.IsAdmin).OrderByDescending(p => p.Point).Take(limit).ToList();
            if (list != null)
                list.ForEach(p => p.Password = string.Empty);
            return list;
        }
    }
}
