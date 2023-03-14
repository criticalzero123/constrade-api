using ConstradeApi.Model.MUser;
using ConstradeApi.Model.MUser.Repository;
using ConstradeApi.Model.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public ReviewController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAverageRate(int userId)
        {
            try
            {
                decimal result = await _userRepository.GetAverage(userId);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, result));
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
        //GET api/<ReviewController>/4/my
        [HttpGet("{otherUserId}/my")]
        public async Task<IActionResult> GetMyReview(int userId, int otherUserId)
        {
            try
            {
                var reviews = await _userRepository.GetMyReviews(userId, otherUserId);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, reviews));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        //GET api/<UserController>
        [HttpGet("{otherUserId}/other")]
        public async Task<IActionResult> GetOtherReviews(int userId, int otherUserId)
        {
            try
            {
                var reviews = await _userRepository.GetReviews(userId, otherUserId);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, reviews));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet("{userId}/available")]
        public async Task<IActionResult> GetNotRated(int userId, int visitorId)
        {
            try
            {
                var available = await _userRepository.GetNotRated(visitorId, userId);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, available));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        //POST api/<Usercontroller>
        [HttpPost]
        public async Task<IActionResult> AddReview(int reviewerId, [FromBody] UserReviewModel userReviewModel)
        {
            try
            {
                bool flag = await _userRepository.AddReview(reviewerId, userReviewModel);

                if (!flag) return BadRequest("Transaction is not found or You already Reviewed");

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, flag));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex));
            }
        }




    }
}
