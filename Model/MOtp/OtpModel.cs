using ConstradeApi.Entity;

namespace ConstradeApi.Model.MOtp
{
    public class OtpModel
    {
        public int OtpId { get; set; }
        public string SendTo { get; set; } = string.Empty;
        public string OTP { get; set; } = string.Empty;
        public DateTime ExpirationTime { get; set; }
    }

   
}
