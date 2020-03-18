using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win.Entities.Dto
{
    public class ProductCommentsViewModel
    {
        public string NickName { get; set; }
        public string PhotoUrl { get; set; }
        public string Text { get; set; }
        public int ProductId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
