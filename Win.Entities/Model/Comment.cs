using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Win.Entities.Model
{
    [Table("rkdb.Comment")]
    public partial class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
