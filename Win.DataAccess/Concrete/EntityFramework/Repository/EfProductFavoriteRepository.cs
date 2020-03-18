using System;
using System.Collections.Generic;
using System.Linq;
using Win.DataAccess.Abstract;
using Win.Entities.Dto;
using Win.Entities.Enum;
using Win.Entities.Model;

namespace Win.DataAccess.Concrete.EntityFramework.Repository
{
    public class EfProductFavoriteRepository : IProductFavoriteDal
    {
        private Context context = new Context();

        public int Add(ProductFavorite entity)
        {
            var dbData = Get(entity.UserID, entity.ProductID);
            if (dbData == null)
            {
                entity.CreatedDate = DateTime.Now;
                entity.AuditDate = DateTime.Now;
                entity.AuditStatus = (short)AuditStatus.created;
                context.ProductFavorite.Add(entity);
                context.SaveChanges();
                return entity.Id;
            }
            else
            {
                Delete(dbData.Id);
                return -1;
            }
        }
        public ProductFavorite Get(int Id)
        {
            return context.ProductFavorite.FirstOrDefault(x => x.AuditStatus != (short)AuditStatus.deleted && x.Id == Id);
        }
        public ProductFavorite Get(int userId, int productId)
        {
            return context.ProductFavorite.FirstOrDefault(x => x.AuditStatus != (short)AuditStatus.deleted && x.UserID == userId && x.ProductID == productId);
        }
        public void Delete(int id)
        {
            var dbEntity = Get(id);
            dbEntity.AuditDate = DateTime.Now;
            dbEntity.AuditStatus = (short)AuditStatus.deleted;
            context.SaveChanges();
        }
        public List<Product> GetProductsByUserId(int userId)
        {
            return (from up in context.ProductFavorite
                    join p in context.Product
                    on up.ProductID equals p.Id
                    where up.UserID == userId && up.AuditStatus != (short)AuditStatus.deleted
                    select p).ToList();
        }
        public List<string> GetUsersByProductId(int productId)
        {
            return (from up in context.ProductFavorite
                    join u in context.UserInfo
                    on up.UserID equals u.Id
                    where up.ProductID == productId && up.AuditStatus != (short)AuditStatus.deleted
                    select u.NickName).ToList();
        }
    }
}