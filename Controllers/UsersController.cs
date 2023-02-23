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

        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ResponseType responseType = ResponseType.Success;

            try
            {
                UserAndPersonModel? user = await _userRepository.Get(id);
                if (user == null) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(responseType, user));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserAndPersonModel userModel)
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


        //PUT api/<UserController/person
        [HttpPut("person")]
        public async Task<IActionResult> UpdatePersonInfo([FromBody] UserAndPersonModel info)
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
