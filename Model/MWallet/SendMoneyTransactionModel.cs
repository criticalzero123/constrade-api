namespace ConstradeApi.Model.MWallet
{
    public class SendMoneyTransactionModel
    {
        public int SendMoneyTransactionId { get; set; }
        public int SenderWalletId { get; set; }
        public int ReceiverWalletId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
