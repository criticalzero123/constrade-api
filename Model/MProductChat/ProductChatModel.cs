namespace ConstradeApi.Model.MProductChat
{
    public class ProductChatModel
    {
   
        public int ProductChatId { get; set; }
        public int UserId1 { get; set; }
        public int UserId2 { get; set; }
        public int ProductId { get; set; }
        public string LastMessage { get; set; } = string.Empty;
        public DateTime LastMessageDate { get; set; }
        public DateTime ChatCreated { get; set; }
    }
}
