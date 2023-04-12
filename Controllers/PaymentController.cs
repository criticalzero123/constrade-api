using ConstradeApi.Model.Response;
using ConstradeApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {

        private readonly IStripeClient client;
        public PaymentController()
        {
            client = new StripeClient(StripeSecretKey.GetStripeSecretKey());
        }

        [HttpPost("stripe/create-payment-intent")]
        public async Task<IActionResult> CreatePaymentIntent(long amount)
        {
            try
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = amount*100,
                    Currency = "PHP",
                };
                var service = new PaymentIntentService(client);
                var paymentIntent = await service.CreateAsync(options);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, new { paymentIntent.ClientSecret }));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
