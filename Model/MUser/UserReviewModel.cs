
namespace ConstradeApi.Model.MUser
{
    public class UserReviewModel
    {

        public int ReviewId { get; set; }
        public int TransactionRefId { get; set; }
        public short Rate { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
    }
}
