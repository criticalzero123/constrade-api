namespace ConstradeApi.Model.MBoostProduct
{
    public class BoostProductModel
    {
        public int BoostProductId { get; set; }
        public int ProductId { get; set; }
        public int DaysBoosted { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime DateTimeExpired { get; set; }
        public DateTime DateBoosted { get; set; }
    }
}
