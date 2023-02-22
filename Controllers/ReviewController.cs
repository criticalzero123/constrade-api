using ConstradeApi.Model.MUser;
using ConstradeApi.Model.MUser.Repository;
using ConstradeApi.Model.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public ReviewController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Getting the Reviews of the user and User Review
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="myReview">if true the reviewed by the user will be fetch</param>
        /// <param name="review">if true the reviews of the user will be fetch</param>
        /// <returns>object</returns>
        //GET api/<ReviewController>/4/all
        [HttpGet("{userId}/all")]
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

        //GET api/<ReviewController>/4/my
        [HttpGet("{userId}/my")]
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

        //POST api/<Usercontroller>
        [HttpPost("{userId}")]
        public async Task<IActionResult> AddReview(int uid, [FromBody] UserReviewModel userReviewModel)
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

        //GET api/<UserController>
        [HttpGet("{userId}")]
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
    }
}
