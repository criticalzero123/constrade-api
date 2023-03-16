using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ConstradeApi.Enums;

namespace ConstradeApi.VerificationModel.MValidIdRequest
{
    public class ValidIdRequestModel
    {
        public int ValidIdRequestId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public ValidIdType ValidationType { get; set; }
        public string ValidIdNumber { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
