using ConstradeApi.Enums;
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
                OtpResponseType flag = await _otpRepository.VerifyOtpCode(user, code);

                if (flag == OtpResponseType.NotFound) return NotFound(ResponseHandler.GetApiResponse(ResponseType.NotFound, $"{flag}"));
                if (flag == OtpResponseType.Expired) return Ok(ResponseHandler.GetApiResponse(ResponseType.Failure, $"{flag}"));
                if (flag == OtpResponseType.WrongCode) return Ok(ResponseHandler.GetApiResponse(ResponseType.Failure, $"{flag}"));

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, flag));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPut("resend/email/{userValue}")]
        public async Task<IActionResult> ResendOtp(string userValue)
        {
            try
            {
                var flag = await _otpRepository.ResendOtpCode(userValue);

                if (flag == OtpResponseType.NotFound) return NotFound();
                if(flag == OtpResponseType.Active)  return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, $"{flag}"));

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, $"{flag}"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
                throw;
            }
        }
    }
}
