namespace ConstradeApi.Model.MUser
{
    public class PersonModel
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int? AddressReferenceId { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? PhoneNumber { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
    }
}
