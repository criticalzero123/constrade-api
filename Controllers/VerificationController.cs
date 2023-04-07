using ConstradeApi.Model.Response;
using ConstradeApi.VerificationEntity;
using ConstradeApi.VerificationModel.MValidIdRequest;
using ConstradeApi.VerificationModel.MValidIdRequest.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class VerificationController : ControllerBase
    {
        private readonly IValidIdRequestRepository _validIdRepo;

        public VerificationController(IValidIdRequestRepository validIdRepo)
        {
            _validIdRepo = validIdRepo;
        }

        [Authorize]
        [HttpPost("submit")]
        public async Task<IActionResult> SendRequest([FromBody] ValidIdRequestModel info)
        {
            try
            {
                bool flag = await _validIdRepo.SubmitValidId(info);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, flag));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetRequests(int userId)
        {
            try
            {
                var request = await _validIdRepo.GetValidationRequests(userId);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, request));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));

            }
        }
    }
}
