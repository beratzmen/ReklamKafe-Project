using System;
using System.Collections.Generic;
using System.Linq;
using Win.DataAccess.Abstract;
using Win.Entities.Enum;
using Win.Entities.Model;

namespace Win.DataAccess.Concrete.EntityFramework.Repository
{
    public class EfProductRepository : IProductDal
    {
        private Context context = new Context();

        public int Add(Product entity)
        {
            entity.AuditStatus = (short)AuditStatus.created;
            entity.AuditDate = DateTime.Now;
            entity.DateOfSold = DateTime.Now;
            entity.CreatedDate = DateTime.Now;
            context.Product.Add(entity);
            context.SaveChanges();
            return entity.Id;
        }

        public void Delete(int Id)
        {
            Product productToUpdate = Get(Id);
            productToUpdate.AuditDate = DateTime.Now;
            productToUpdate.AuditStatus = (short)AuditStatus.deleted;
            context.SaveChanges();
        }

        public Product Get(int Id)
        {
            return context.Product.FirstOrDefault(x => x.AuditStatus != (short)AuditStatus.deleted && x.Id == Id);
        }

        public List<Product> GetAll()
        {
            return context.Product.Where(p => p.AuditStatus != (short)AuditStatus.deleted).ToList();
        }

        public void Update(Product entity)
        {
            Product productToUpdate = Get(entity.Id);
            productToUpdate.Name = entity.Name;
            productToUpdate.Description = entity.Description;
            productToUpdate.Price = entity.Price;
            productToUpdate.Amount = entity.Amount;
            productToUpdate.Upload1 = entity.Upload1;
            productToUpdate.Upload2 = entity.Upload2;
            productToUpdate.Upload3 = entity.Upload3;
            productToUpdate.AuditDate = DateTime.Now;
            productToUpdate.AuditStatus = (short)AuditStatus.updated;
            context.SaveChanges();
        }

        public List<Product> GetLastAdded(int limit)
        {
            var products = GetAll();
            if (products == null)
                return null;
            else
                return products.OrderByDescending(p => p.Id).Take(limit).ToList();
        }

        public List<Product> GetLastSolded(int limit)
        {
            //var products = context.Product.Where(p => p.AuditStatus != (short)AuditStatus.deleted && p.IsSold).ToList();
            //if (products == null)
            //    return null;
            //else
            //    return products.OrderByDescending(p => p.DateOfSold).Take(limit).ToList();
            return null;
        }

        public List<Product> GetProductsByActiveStatus(bool isActive)
        {
            return context.Product.Where(p => p.AuditStatus != (short)AuditStatus.deleted && p.IsActive == isActive).ToList();
        }
        /// <summary>
        /// Bu metot 3 ayrı metodla veri tabanına gidilmesini önlemek için yazılmıştır.
        /// </summary>
        /// <param name="lastAddedLimit"></param>
        /// <param name="lastSoldedLimit"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public Tuple<List<Product>, List<Product>, List<Product>> GetMainPageSliderItems(int lastAddedLimit, bool isActive)
        {
            var products = GetAll();
            if (products == null)
                return null;
            else
            {
                var lastAddedList = products.OrderByDescending(p => p.Id).Take(lastAddedLimit).ToList();
                var activeProducts = products.Where(p => p.IsActive == isActive && p.Amount > 0).ToList();
                var topProducts = products.OrderByDescending(p => p.DisplayCount).Take(8).ToList();
                return new Tuple<List<Product>, List<Product>, List<Product>>(lastAddedList, activeProducts, topProducts);
            }
        }

        public List<Product> GetProductByFiltering(List<int> categoryIdList, float minPrice, float maxPrice, int sortingValue)
        {
            #region NOTE
            //return (from p in context.Product
            //        join pc in context.ProductCategory on p.Id equals pc.ProductID
            //        join ct in context.Category on pc.CategoryID equals ct.Id
            //        where (p.AuditStatus != (short)AuditStatus.deleted) && (ct.AuditStatus != (short)AuditStatus.deleted) && (ct.Id == categoryId)
            //        select p).ToList();

            // where (ct.Id == categoryId)
            #endregion
            return SortProduct((from p in context.Product
                                join pc in context.ProductCategory on p.Id equals pc.ProductID
                                join ct in context.Category on pc.CategoryID equals ct.Id
                                where (p.AuditStatus != (short)AuditStatus.deleted) && (p.Price >= minPrice && p.Price <= maxPrice) && (ct.AuditStatus != (short)AuditStatus.deleted) && (categoryIdList.Contains(ct.Id))
                                select p).Distinct().ToList(), sortingValue);//.GroupBy(p => p.Id).Select(grp => grp.FirstOrDefault()).ToList();
        }
        public List<Product> GetProductByPrice(float minPrice, float maxPrice, int sortingValue)
        {
            return SortProduct(context.Product.Where(p => p.AuditStatus != (short)AuditStatus.deleted && (p.Price >= minPrice && p.Price <= maxPrice)).ToList(), sortingValue);
        }
        public void UpdateAmount(int productId, short amount)
        {
            Product updPrd = Get(productId);
            updPrd.Amount -= amount;
            updPrd.AuditDate = DateTime.Now;
            updPrd.AuditStatus = (short)AuditStatus.updated;
            context.SaveChanges();
        }
        public List<Product> SortProduct(List<Product> products, int val)
        {
            if (val <= 1)
                return products.OrderByDescending(x => x.CreatedDate).ToList();
            else if (val == 2)
                return products;
            else if (val == 3)
                return products.OrderByDescending(x => x.Price).ToList();
            else if (val == 4)
                return products.OrderBy(x => x.Price).ToList();
            else
                return products.OrderByDescending(x => x.CreatedDate).ToList();
        }

        public void DisplayCounter(int id)
        {
            Product counter = Get(id);
            counter.DisplayCount++;
            context.SaveChanges();
        }

        public List<Product> TopProductDisplay()
        {
            return (from tP in context.Product
                    orderby tP.DisplayCount descending
                    select tP).Take(8).ToList();
        }

        public List<Product> Search(string search)
        {
            var searchProduct = GetAll();
            return searchProduct.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToList(); ;
        }
    }
}
