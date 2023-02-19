using ConstradeApi.Model.MOtp;
using ConstradeApi.Model.MOtp.Repository;
using ConstradeApi.Model.Response;
using Microsoft.AspNetCore.Mvc;

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        private readonly IOtpRepository _otpRepository;

        public OtpController(IOtpRepository repository)
        {
            _otpRepository = repository;
        }

        [HttpPost("email")]
        public async Task<IActionResult> GenerateOtpEmail([FromBody] OtpModel otpModel)
        {
            try
            {
                bool flag = await _otpRepository.GenerateOtpCode(otpModel.SendTo);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, flag));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet("verify")]
        public async Task<IActionResult> VerifyOtp(string user, string code)
        {
            try
            {
                bool flag = await _otpRepository.VerifyOtpCode(user, code);

                if (!flag) return BadRequest();

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, flag));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
