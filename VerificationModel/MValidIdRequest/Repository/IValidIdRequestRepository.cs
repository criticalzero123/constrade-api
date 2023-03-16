namespace ConstradeApi.VerificationModel.MValidIdRequest.Repository
{
    public interface IValidIdRequestRepository
    {
        Task<bool> SubmitValidId(ValidIdRequestModel info);
        //Task<IEnumerable<GetRequestAdmin>> GetValidationRequests();
    }
}
