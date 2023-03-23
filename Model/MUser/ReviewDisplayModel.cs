namespace ConstradeApi.Model.MUser
{
    public class ReviewDisplayModel
    {
        public int ReviewId { get; set; }
        public int TransactionId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public short Rate { get; set; }
        public DateTime Date { get; set; }

    }
}
