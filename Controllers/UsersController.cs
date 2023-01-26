using ConstradeApi.Entity;
using ConstradeApi.Model;
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
                IEnumerable<UserModel> users = _dbHelper.GetUsers();
                
                if (!users.Any())
                {
                    responseType = ResponseType.NotFound;
                }

                return Ok(ResponseHandler.GetApiResponse(responseType, users));
            }
            catch (Exception ex)
            {

                responseType = ResponseType.Failure;
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
                UserModel? user= _dbHelper.GetUser(id);
                if (user == null)
                {
                    responseType = ResponseType.NotFound;
                    return NotFound();
                }

                return Ok(ResponseHandler.GetApiResponse(responseType, user));
            } catch(Exception ex)
            {
                responseType = ResponseType.Failure;
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
                _dbHelper.SaveUser(userModel);

                return Ok(ResponseHandler.GetApiResponse(response, userModel));
            }catch( Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException));
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
