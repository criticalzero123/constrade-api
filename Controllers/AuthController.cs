using ConstradeApi.Model.MUser;
using ConstradeApi.Model.MUser.Repository;
using ConstradeApi.Model.MUserAuthorize.Repository;
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
        private readonly IUserAuthorizeRepository _sessionRepository;

        public AuthController(IUserRepository userRepository, IUserAuthorizeRepository userAuthorize)
        {
            _userRepository = userRepository;
            _sessionRepository = userAuthorize;
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
                UserAndPersonModel? user = await _userRepository.LoginByGoogle(data.Email);

                if (user == null) return Unauthorized();

                var sessionInfo = await _sessionRepository.CreateApiKeyAsync(user.User.UserId);


                return Ok(ResponseHandler.GetApiResponse(responseType, new { user, sessionInfo.Token }));
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
                UserAndPersonModel? user = await _userRepository.LoginByEmailAndPassword(info);

                if (user == null) return Unauthorized();

                var sessionInfo = await _sessionRepository.CreateApiKeyAsync(user.User.UserId);

                return Ok(ResponseHandler.GetApiResponse(responseType, new { user, sessionInfo.Token }));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
