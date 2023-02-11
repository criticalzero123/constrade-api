using ConstradeApi.Entity;
using ConstradeApi.Model.MSubcription;
using ConstradeApi.Model.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly DbHelperSubscription _dbHelper;
        public SubscriptionController(DataContext dataContext)
        {
            _dbHelper= new DbHelperSubscription(dataContext);
        }

        [HttpPut("cancel/{uid}")]
        public async Task<IActionResult> CancelSubscribeUser(int uid)
        {
            try
            {
                bool sub = await _dbHelper.CancelPremium(uid);

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
                bool sub = await _dbHelper.SubscribePremium(uid);

                if (!sub) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, sub));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet("user/{uid}")]
        public IActionResult GetUserSubscription(int uid)
        {
            try
            {
                var sub = _dbHelper.GetSubscriptionByUserId(uid);

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
                var sub = await _dbHelper.GetSubscriptionHistoryByUserId(uid);

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
