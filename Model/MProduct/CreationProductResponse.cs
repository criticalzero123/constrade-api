using ConstradeApi.Enums;

namespace ConstradeApi.Model.MProduct
{
    public class CreationProductResponse
    {
        public ProductAddResponseType Response { get; set; }
        public int ProductId { get; set; }
        public int PosterUserId { get; set; }
    }
}
