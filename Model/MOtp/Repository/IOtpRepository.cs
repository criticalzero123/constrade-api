using ConstradeApi.Enums;

namespace ConstradeApi.Model.MOtp.Repository
{
    public interface IOtpRepository
    {
        Task<bool> GenerateOtpCode(string userValue);
        Task<OtpResponseType> VerifyOtpCode(string userValue, string code);
        Task<OtpResponseType> ResendOtpCode(string userValue);
    }
}
