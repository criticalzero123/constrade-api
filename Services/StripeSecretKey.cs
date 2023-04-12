namespace ConstradeApi.Services
{
    public class StripeSecretKey
    {
        private readonly IConfiguration _configuration;
        private string KEY { get; set; }
        public StripeSecretKey()
        {
            _configuration = new ConfigurationBuilder()
            .AddUserSecrets<StripeSecretKey>()
            .Build();

            KEY = _configuration["StripeSecretKey"]!;
        }

        public static string GetStripeSecretKey()
        {
            StripeSecretKey sp = new StripeSecretKey();
            return sp.KEY;
        }
    }
}
