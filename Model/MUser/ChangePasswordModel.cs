namespace ConstradeApi.Model.MUser
{
    public class ChangePasswordModel
    {
        public string Email { get; set; } = string.Empty;

        public string NewPassword { get; set; } = string.Empty;
    }
}
