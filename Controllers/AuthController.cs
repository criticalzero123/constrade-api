using ConstradeApi.Enums;
using ConstradeApi.Model.MOtp;
using ConstradeApi.Model.MOtp.Repository;
using ConstradeApi.Model.MSubcription.Repository;
using ConstradeApi.Model.MUser;
using ConstradeApi.Model.MUser.Repository;
using ConstradeApi.Model.MUserApiKey.Repository;
using ConstradeApi.Model.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IApiKeyRepository _sessionRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IOtpRepository _otpRepository;

        public AuthController(IUserRepository userRepository, IApiKeyRepository userAuthorize, ISubscriptionRepository subscription, IOtpRepository otpRepository)
        {
            _userRepository = userRepository;
            _sessionRepository = userAuthorize;
            _subscriptionRepository = subscription;
            _otpRepository = otpRepository;
          
        }

        //GET: api/<AuthController>
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Hello World");
        }

        // GET: api/<AuthController>/check/email/johndoe@test.com
        [HttpGet("check/email/{email}")]
        public async Task<IActionResult> CheckUserByEmail(string email)
        {
            ResponseType responseType = ResponseType.Success;
            try
            {
                bool userExist = await _userRepository.CheckEmailExist(email);

                return Ok(ResponseHandler.GetApiResponse(responseType, userExist));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // GET: api/<AuthController>/check/phone/6399999999
        [HttpGet("check/phone/{phone}")]
        public async Task<IActionResult> CheckUserByPhone(string phone)
        {
            ResponseType responseType = ResponseType.Success;
            try
            {
                bool userExist = await _userRepository.CheckPhoneExist(phone);

                return Ok(ResponseHandler.GetApiResponse(responseType, userExist));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // Put api/<AuthController>/login/google/johndoe@test.com
        [HttpPut("login/google")]
        public async Task<IActionResult> LoginByGoogleAuth([FromBody] UserLoginInfoModel data)
        {
            ResponseType responseType = ResponseType.Success;

            try
            {
                UserAndPersonModel? userInfo = await _userRepository.LoginByGoogle(data.Email);

                if (userInfo == null) return Unauthorized();

                var sessionInfo = await _sessionRepository.CreateApiKeyAsync(userInfo.User.UserId);


                return Ok(ResponseHandler.GetApiResponse(responseType, new { userInfo, sessionInfo.Token }));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // PUT api/<AuthController>/login
        [HttpPut("login")]
        public async Task<IActionResult> LoginByEmailAndPassword([FromBody] UserLoginInfoModel info)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;
                UserAndPersonModel? userInfo = await _userRepository.LoginByEmailAndPassword(info);

                if (userInfo == null) return Unauthorized();

                var sessionInfo = await _sessionRepository.CreateApiKeyAsync(userInfo.User.UserId);

                return Ok(ResponseHandler.GetApiResponse(responseType, new { userInfo, sessionInfo.Token }));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }


        // POST api/<AuthController>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserAndPersonModel userModel)
        {
            try
            {
                ResponseType response = ResponseType.Success;
                int uid = await _userRepository.Save(userModel);
                await _subscriptionRepository.CreateSubscription(uid);

                return Ok(ResponseHandler.GetApiResponse(response, userModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // POST api/<AuthController>/otp/{email}
        [HttpPost("otp/email")]
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

        //GET api/<AuthController>/otp/verify?user={user}&code={code}
        [HttpGet("otp/verify")]
        public async Task<IActionResult> VerifyOtp(string user, string code)
        {
            try
            {
                OtpResponseType flag = await _otpRepository.VerifyOtpCode(user, code);

                if (flag == OtpResponseType.NotFound) return NotFound(ResponseHandler.GetApiResponse(ResponseType.NotFound, $"{flag}"));
                if (flag == OtpResponseType.Expired) return Ok(ResponseHandler.GetApiResponse(ResponseType.Failure, $"{flag}"));
                if (flag == OtpResponseType.WrongCode) return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, $"{flag}"));

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, $"{flag}"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        //GET api/<AuthController>/otp/resend/email/{userValue}

        [HttpPut("otp/resend/email/{userValue}")]
        public async Task<IActionResult> ResendOtp(string userValue)
        {
            try
            {
                var flag = await _otpRepository.ResendOtpCode(userValue);

                if (flag == OtpResponseType.NotFound) return NotFound();
                if (flag == OtpResponseType.Active) return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, $"{flag}"));

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
