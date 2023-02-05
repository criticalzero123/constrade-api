
namespace ConstradeApi.Model.MProduct
{
    public class ProductCommentModel
    {
        public int ProductCommentId { get; set; }

        public int ProductId { get; set; }
     
        public int UserId { get; set; }

        public string Comment { get; set; } = string.Empty;
     
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }

    public class ProductUpdateNewComment
    {
        public string NewComment { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}
