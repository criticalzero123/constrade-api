using ConstradeApi.Entity;
using ConstradeApi.Enums;
using ConstradeApi.Model.MSubcription;
using ConstradeApi.Model.MSubcription.Repository;
using ConstradeApi.Model.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        public SubscriptionController(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

       
        [HttpPut("cancel/{uid}")]
        public async Task<IActionResult> CancelSubscribeUser(int uid)
        {
            try
            {
                bool sub = await _subscriptionRepository.CancelPremium(uid);

                if (!sub) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, sub));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

    
        [HttpPut("subscribe/{uid}")]
        public async Task<IActionResult> SubscribeUser(int uid)
        {
            try
            {
                SubscriptionResponseType sub = await _subscriptionRepository.SubscribePremium(uid);

                if (SubscriptionResponseType.Success != sub) 
                       return Ok(ResponseHandler.GetApiResponse(ResponseType.Failure, $"{sub}"));
                

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, $"{sub}"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

      
        [HttpGet("user/{uid}")]
        public async Task<IActionResult> GetUserSubscription(int uid)
        {
            try
            {
                var sub = await _subscriptionRepository.GetSubscriptionByUserId(uid);

                if (sub == null) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, sub));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

   
        [HttpGet("history/user/{uid}")]
        public async Task<IActionResult> GetUserSubscriptionHistory(int uid)
        {
            try
            {
                var sub = await _subscriptionRepository.GetSubscriptionHistoryByUserId(uid);

                if (sub == null) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, sub));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
