using ConstradeApi.Entity;
using ConstradeApi.Model.MSubcription;
using ConstradeApi.Model.MSubcription.Repository;
using ConstradeApi.Model.MUser;
using ConstradeApi.Model.MUser.Repository;
using ConstradeApi.Model.MUserAuthorize.Repository;
using ConstradeApi.Model.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUserAuthorizeRepository _sessionRepository; 
        public UsersController(IUserRepository userRepository, ISubscriptionRepository subscriptionRepository, IUserAuthorizeRepository sessionRepository)
        {
            _userRepository = userRepository;
            _subscriptionRepository = subscriptionRepository;
            _sessionRepository = sessionRepository;
        }

        // GET: api/<UserController>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ResponseType responseType = ResponseType.Success;
            try
            {
                IEnumerable<UserModel> users = await _userRepository.GetAll();
                

                return Ok(ResponseHandler.GetApiResponse(responseType, users));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // GET: api/<UserController>/check/email/johndoe@test.com
        [Route("check/email/{email}")]
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

        // GET: api/<UserController>/check/phone/6399999999
        [Route("check/phone/{phone}")]
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

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ResponseType responseType = ResponseType.Success;

            try
            {
                UserModel? user= await _userRepository.Get(id);
                if (user == null) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(responseType, user));
            } catch(Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
        // Put api/<UserController>/login/google/johndoe@test.com
        [HttpPut("login/google")]
        public async Task<IActionResult> LoginByGoogleAuth([FromBody] UserLoginInfoModel data)
        {
            ResponseType responseType = ResponseType.Success;

            try
            {
                UserInfoModel? user = await _userRepository.LoginByGoogle(data.Email);

                if (user == null) return Unauthorized();

                var sessionInfo = await _sessionRepository.CreateApiKeyAsync(user.UserId);


                return Ok(ResponseHandler.GetApiResponse(responseType, new { user, sessionInfo.Token }));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // PUT api/<UserController>/login
        [HttpPut("login")]
        public async Task<IActionResult> LoginByEmailAndPassword([FromBody] UserLoginInfoModel info)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;
                UserInfoModel? user = await _userRepository.LoginByEmailAndPassword(info);

                if (user == null) return Unauthorized();

                var sessionInfo = await _sessionRepository.CreateApiKeyAsync(user.UserId);

                return Ok(ResponseHandler.GetApiResponse(responseType, new { user, sessionInfo.Token}));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserInfoModel userModel)
        {
            try
            {
                ResponseType response= ResponseType.Success;
                int uid = await _userRepository.Save(userModel);
                await _subscriptionRepository.CreateSubscription(uid);

                return Ok(ResponseHandler.GetApiResponse(response, userModel));
            }catch( Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UserModel userModel)
        {
        }

        //GET api/<UserController>/4/favorite
        [HttpGet("{userId}/favorite")]
        public async Task<IActionResult> GetFavorite(int userId)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;

                var result = await _userRepository.GetFavorite(userId);

                if (result.Count() == 0) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(responseType, result));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        //POST api/<UserController>/4/favorite
        [HttpPost("{userId}/favorite")]
        public async Task<IActionResult> AddFavorite(int userId, [FromBody] FavoriteModel favoriteModel)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;

                bool result = await _userRepository.AddFavorite(userId, favoriteModel.ProductId);
                if(!result) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(responseType, favoriteModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex));
            }
        }

        //GET api/<UserController>/4/favorite/4
        [HttpDelete("{userId}/favorite/{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;

                bool result = await _userRepository.DeleteFavorite(id);
                if(!result) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(responseType, id));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex));
            }
        }

        //POST api/<UserController>/4/follow
        [HttpPost("{userId}/follow")]
        public async Task<IActionResult> Follow(int userId, [FromBody] UserFollowModel userFollow)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;
                bool flag = await _userRepository.FollowUser(userId, userFollow.FollowedByUserId);

                if (!flag) responseType = ResponseType.Failure;
                
                return Ok(ResponseHandler.GetApiResponse(responseType, userFollow));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex));
            }
        }

        //GET api/<Usercontroller>/4/follow
        [HttpGet("{userId}/follow")]
        public async Task<IActionResult> GetFollows(int userId)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;

                var followers = await _userRepository.GetUserFollower(userId);
                var follows = await _userRepository.GetUserFollow(userId);

                return Ok(ResponseHandler.GetApiResponse(responseType, new { followers, follows }));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null? ex.InnerException : ex));
            }
        }

        //POST api/<Usercontroller>/review
        [HttpPost("{userId}/review")]
        public async Task<IActionResult> AddReview(int uid,[FromBody] UserReviewModel userReviewModel)
        {
            try
            {
                bool flag = await _userRepository.AddReview(uid, userReviewModel);

                if (!flag) return BadRequest("Transaction is not found or You already Reviewed");

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, flag));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex));
            }
        }

        //GET api/<UserController>/review
        [HttpGet("{userId}/review")]
        public async Task<IActionResult> GetReviews(int userId)
        {
            try
            {
                var reviews = await _userRepository.GetReviews(userId);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, reviews));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        //GET api/<UserController>/review
        [HttpGet("{userId}/review/my")]
        public async Task<IActionResult> GetMyReviews(int userId)
        {
            try
            {
                var reviews = await _userRepository.GetMyReviews(userId);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, reviews));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        /// <summary>
        /// Getting the Reviews of the user and User Review
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="myReview">if true the reviewed by the user will be fetch</param>
        /// <param name="review">if true the reviews of the user will be fetch</param>
        /// <returns>object</returns>
        //GET api/<Usercontroller>/review
        [HttpGet("{userId}/review/all")]
        public async Task<IActionResult> GetAllReviews(int userId)
        {
            try
            {
                var myReviews = await _userRepository.GetMyReviews(userId);
                var reviews = await _userRepository.GetReviews(userId);
                

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, new { myReviews, reviews }));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex));
            }
        }

        //PUT api/<UserController/person
        [HttpPut("person")]
        public async Task<IActionResult> UpdatePersonInfo([FromBody] UserInfoModel info)
        {
            try
            {
                var flag = await _userRepository.UpdatePersonInfo(info);

                if (flag == null) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, flag));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
