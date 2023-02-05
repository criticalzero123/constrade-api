
namespace ConstradeApi.Model.MUser
{
    public class UserReviewModel
    {

        public int ReviewId { get; set; }
        public int TransactionRefId { get; set; }
        public short Rate { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
