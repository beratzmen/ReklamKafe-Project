using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win.Entities.Dto
{
    public class UserProductViewModel
    {
        public string NickName { get; set; }
        public string ProductName { get; set; }
        public short Status { get; set; }
        public int ProductId { get; set; }
        public int Id{ get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
