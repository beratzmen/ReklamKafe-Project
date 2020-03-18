using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win.Entities.Dto;
using Win.Entities.Model;

namespace Win.DataAccess.Abstract
{
    public interface ICommentDal
    {
        List<ProductCommentsViewModel> GetAll(int id);
        void Add(Comment entity);
    }
}
