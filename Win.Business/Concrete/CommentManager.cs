using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win.DataAccess;
using Win.DataAccess.Abstract;
using Win.Entities.Dto;
using Win.Entities.Model;

namespace Win.Business.Concrete
{
    public class CommentManager
    {
        ICommentDal _commentDal;
        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public List<ProductCommentsViewModel> GetAll(int id)
        {
            return _commentDal.GetAll(id);
        }
        public void Add(Comment entity)
        {
            _commentDal.Add(entity);
        }
    }
}
