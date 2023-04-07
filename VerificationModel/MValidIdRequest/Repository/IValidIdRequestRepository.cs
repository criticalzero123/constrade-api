namespace ConstradeApi.VerificationModel.MValidIdRequest.Repository
{
    public interface IValidIdRequestRepository
    {
        Task<ValidIdRequestModel?> GetValidationRequests(int userId);
        Task<bool> SubmitValidId(ValidIdRequestModel info);
    }
}
