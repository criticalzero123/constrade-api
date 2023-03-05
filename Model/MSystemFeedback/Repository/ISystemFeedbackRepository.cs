namespace ConstradeApi.Model.MSystemFeedback.Repository
{
    public interface ISystemFeedbackRepository
    {
        Task<bool> AddSystemFeedback(SystemFeedbackModel info);
    }
}
