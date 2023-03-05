using ConstradeApi.Model.MSystemFeedback;
using ConstradeApi.Model.MSystemFeedback.Repository;
using ConstradeApi.Model.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SystemFeedbackController : ControllerBase
    {
        private readonly ISystemFeedbackRepository _systemFeedbackRepo;

        public SystemFeedbackController(ISystemFeedbackRepository repository)
        {
            _systemFeedbackRepo = repository;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitFeedback([FromBody] SystemFeedbackModel info)
        {
            try
            {
                bool flag = await _systemFeedbackRepo.AddSystemFeedback(info);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, flag));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
