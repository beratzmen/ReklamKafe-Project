using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win.DataAccess.Abstract;
using Win.Entities.Dto;
using Win.Entities.Model;

namespace Win.DataAccess.Concrete.EntityFramework.Repository
{
    public class EfCommentRepository : ICommentDal
    {
        private Context context = new Context();
        public void Add(Comment entity)
        {
            entity.CreatedDate = System.DateTime.Now;
            context.Comment.Add(entity);
            context.SaveChanges();
        }

        public List<ProductCommentsViewModel> GetAll(int id)
        {
            var comments = (from c in context.Comment
                            join ui in context.UserInfo on c.UserId equals ui.Id
                            join p in context.Product on c.ProductId equals p.Id
                            where p.Id == id
                            orderby c.CreatedDate descending
                            select new ProductCommentsViewModel
                            {
                                NickName = ui.NickName,
                                PhotoUrl = ui.PhotoUrl,
                                Text = c.Text,
                                ProductId = c.ProductId,
                                CreatedDate = c.CreatedDate
                            }).ToList();
            //var commentsList = comments.AsQueryable();

            return comments;
        }
    }
}
