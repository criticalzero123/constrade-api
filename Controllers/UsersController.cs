using ConstradeApi.Entity;
using ConstradeApi.Model.MUser;
using ConstradeApi.Model.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DbHelper _dbHelper;
        public UsersController(DataContext dataContext)
        {
            _dbHelper  = new DbHelper(dataContext);
        }

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            ResponseType responseType = ResponseType.Success;
            try
            {
                IEnumerable<UserModel> users = _dbHelper.GetAll();
                
                if (!users.Any()) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(responseType, users));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // GET: api/<UserController>/check/email/johndoe@test.com
        [Route("check/email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
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
        public async Task<IActionResult> GetUserByPhone(string phone)
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

        
        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] UserModel userModel)
        {
            try
            {
                ResponseType response= ResponseType.Success;
                _dbHelper.Save(userModel);

                return Ok(ResponseHandler.GetApiResponse(response, userModel));
            }catch( Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        //GET api/<UserController>/4/favorite
        [HttpGet("{userId}/favorite")]
        public async Task<IActionResult> GetFavorite(int userId)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;

                List<FavoriteModel> result = await _dbHelper.GetFavorite(userId);

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
    }
}
