using System;
using System.Collections.Generic;
using System.Linq;
using Win.DataAccess.Abstract;
using Win.Entities.Dto;
using Win.Entities.Enum;
using Win.Entities.Model;

namespace Win.DataAccess.Concrete.EntityFramework.Repository
{
    public class EfUserProductRepository : IUserProductDal
    {
        private Context context = new Context();

        public int Add(UserProduct entity)
        {
            entity.Status = (short)ProcessStatus.pending;
            entity.CreatedDate = DateTime.Now;
            entity.AuditDate = DateTime.Now;
            entity.AuditStatus = (short)AuditStatus.created;
            context.UserProduct.Add(entity);
            context.SaveChanges();
            return entity.Id;
        }
        public UserProduct Get(int Id)
        {
            return context.UserProduct.FirstOrDefault(x => x.AuditStatus != (short)AuditStatus.deleted && x.Id == Id);
        }
        public List<UserProduct> GetAll()
        {
            return context.UserProduct.Where(p => p.AuditStatus != (short)AuditStatus.deleted).ToList();
        }
        public List<UserProduct> GetAll(int userId)
        {
            return context.UserProduct.Where(p => (p.AuditStatus != (short)AuditStatus.deleted) && p.UserID == userId).ToList();
        }
        public void Update(UserProduct entity)
        {
            var dbEntity = Get(entity.Id);
            dbEntity.AuditStatus = (short)AuditStatus.updated;
            dbEntity.AuditDate = DateTime.Now;
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            var dbEntity = Get(id);
            dbEntity.AuditDate = DateTime.Now;
            dbEntity.AuditStatus = (short)AuditStatus.deleted;
            context.SaveChanges();
        }
        public void ProcessCompleted(int id)
        {
            var entity = Get(id);
            entity.Status = (short)ProcessStatus.completed;
            Update(entity);
        }
        public List<UserProductViewModel> GetProductsByUserId(int userId)
        {
            return (from up in context.UserProduct
                    join p in context.Product on up.ProductID equals p.Id
                    join ui in context.UserInfo on up.UserID equals ui.Id
                    where up.AuditStatus != 3 && up.UserID.Equals(userId)
                    orderby up.CreatedDate descending
                    select new UserProductViewModel
                    {
                        Id = up.Id,
                        UserId = up.UserID,
                        ProductId = up.ProductID,
                        NickName = ui.NickName,
                        ProductName = p.Name,
                        Status = up.Status,
                        CreatedDate = up.CreatedDate
                    }).ToList();
        }
        public List<string> GetUsersByProductId(int productId)
        {
            return (from up in context.UserProduct
                    join u in context.UserInfo
                    on up.UserID equals u.Id
                    where up.ProductID == productId
                    select u.NickName).ToList();
        }

        public List<UserProductViewModel> GetUserProduct()
        {
            var products = (from up in context.UserProduct
                            join p in context.Product on up.ProductID equals p.Id
                            join ui in context.UserInfo on up.UserID equals ui.Id
                            where up.AuditStatus != 3
                            orderby up.CreatedDate descending
                            select new UserProductViewModel
                            {
                                Id = up.Id,
                                UserId = up.UserID,
                                ProductId = up.ProductID,
                                NickName = ui.NickName,
                                ProductName = p.Name,
                                Status = up.Status,
                                CreatedDate = up.CreatedDate
                            }).ToList();

            return products;
        }

        public UserProductViewModel GetUserProduct(int id)
        {
            var products = (from up in context.UserProduct
                            join p in context.Product on up.ProductID equals p.Id
                            join ui in context.UserInfo on up.UserID equals ui.Id
                            where up.AuditStatus != 3 && up.Id == id
                            select new UserProductViewModel
                            {
                                Id = up.Id,
                                UserId = up.UserID,
                                ProductId = up.ProductID,
                                NickName = ui.NickName,
                                ProductName = p.Name,
                                Status = up.Status,
                                CreatedDate = up.CreatedDate
                            }).FirstOrDefault();

            return products;
        }

        public UserProductViewModel GetUserByProductId(int id, int userId)
        {
            var products = (from up in context.UserProduct
                            join p in context.Product on up.ProductID equals p.Id
                            join ui in context.UserInfo on up.UserID equals ui.Id
                            where (up.AuditStatus != 3 && up.ProductID == id && ui.Id == userId) || up.Id == id
                            select new UserProductViewModel
                            {
                                Id = up.Id,
                                UserId = up.UserID,
                                ProductId = up.ProductID,
                                NickName = ui.NickName,
                                ProductName = p.Name,
                                Status = up.Status,
                                CreatedDate = up.CreatedDate
                            }).FirstOrDefault();

            return products;
        }
    }
}
