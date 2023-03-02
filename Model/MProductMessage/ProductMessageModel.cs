using ConstradeApi.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConstradeApi.Model.MProductMessage
{
    public class ProductMessageModel
    {
        public int ProductMessageId { get; set; }
        public int ProductChatId { get; set; }
        public int SenderId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime DateSent { get; set; }
    }
}
