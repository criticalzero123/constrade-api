using ConstradeApi.Entity;
using ConstradeApi.Model.MSubcription;
using ConstradeApi.Model.MUser;
using ConstradeApi.Model.Response;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DbHelperUser _dbHelper;
        private readonly DbHelperSubscription _dbHelperSubscription;
        public UsersController(DataContext dataContext)
        {
            _dbHelper  = new DbHelperUser(dataContext);
            _dbHelperSubscription = new DbHelperSubscription(dataContext);
        }

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            ResponseType responseType = ResponseType.Success;
            try
            {
                IEnumerable<UserModel> users = _dbHelper.GetAll();
                

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
                bool userExist = await _dbHelper.CheckEmailExist(email);

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
                bool userExist = await _dbHelper.CheckPhoneExist(phone);

                return Ok(ResponseHandler.GetApiResponse(responseType, userExist));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ResponseType responseType = ResponseType.Success;

            try
            {
                UserModel? user= _dbHelper.Get(id);
                if (user == null) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(responseType, user));
            } catch(Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // GET api/<UserController>/login/google/johndoe@test.com
        [HttpGet("login/email/{email}")]
        public IActionResult LoginByGoogleAuth(string email)
        {
            ResponseType responseType = ResponseType.Success;

            try
            {
                UserModel? user = _dbHelper.GetUserInfoByEmail(email);

                return Ok(ResponseHandler.GetApiResponse(responseType, user));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }


        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRegistrationInfoModel userModel)
        {
            try
            {
                ResponseType response= ResponseType.Success;
                int uid = await _dbHelper.Save(userModel);
                await _dbHelperSubscription.CreateSubscription(uid);

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
        public IActionResult GetFavorite(int userId)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;

                List<FavoriteModel> result =  _dbHelper.GetFavorite(userId);

                if (result.Count == 0) return NotFound();

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

                bool result = await _dbHelper.AddFavorite(userId, favoriteModel.ProductId);
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

                bool result = await _dbHelper.DeleteFavorite(id);
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
        public IActionResult Follow(int userId, [FromBody] UserFollowModel userFollow)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;
                bool flag = _dbHelper.FollowUser(userId, userFollow.FollowedByUserId);

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
        public IActionResult GetFollows(int userId)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;

                List<UserFollowModel> followers = _dbHelper.GetUserFollower(userId);
                List<UserFollowModel> follows = _dbHelper.GetUserFollow(userId);

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
                bool flag = await _dbHelper.AddReview(uid, userReviewModel);

                if (!flag) return BadRequest("Transaction is not found or You already Reviewed");

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, flag));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex));
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
                //UserModel?  userExist =  _dbHelper.Get(userId);

                //if (userExist == null) return NotFound();

                var myReviews = await _dbHelper.GetMyReviews(userId);
                var reviews = await _dbHelper.GetReviews(userId);
                

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, new { myReviews, reviews }));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex));
            }
        }
    }
}
