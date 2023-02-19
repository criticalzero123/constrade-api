namespace ConstradeApi.Model.MOtp.Repository
{
    public interface IOtpRepository
    {
        Task<bool> GenerateOtpCode(string userValue);
        Task<bool> VerifyOtpCode(string userValue, string code);
    }
}
