
using System.Security.Cryptography;
using System.Text;

namespace ConstradeApi.Services.OTP
{
    public class OtpService
    {
        private readonly IConfiguration _configuration;
        private string OTP_KEY {get; set;}

        public OtpService()
        {
            _configuration = new ConfigurationBuilder()
                .AddUserSecrets<OtpService>()
                .Build();

            OTP_KEY = _configuration["OtpSecretKeyValue"]!;
        }

        /// <summary>
        /// Generate a 6 digits Hash number
        /// </summary>
        /// <returns>string otp (eg. "616161")</returns>
        public static string GenerateOtp()
        {
            OtpService service = new OtpService();

            //6 digit
            int otpNumber = new Random().Next(100000,999999);

            string secretKey = service.OTP_KEY;

            // Combine the secret key and the OTP number
            string message = string.Format("{0}:{1}", secretKey, otpNumber);

            // Convert the message to bytes
            byte[] bytes = Encoding.UTF8.GetBytes(message);

            // Use a hash function to generate a unique hash
            SHA256 sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(bytes);

            // Convert the hash to a 6-digit OTP string
            string otp = Math.Abs(BitConverter.ToInt32(hash, 0)).ToString().Substring(0, 6);

            return otp;
        }
    }
}
