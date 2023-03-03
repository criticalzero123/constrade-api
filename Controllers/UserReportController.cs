using ConstradeApi.Model.MUserReport;
using ConstradeApi.Model.MUserReport.Repositories;
using ConstradeApi.Model.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserReportController : ControllerBase
    {
        private readonly IUserReportRepository _userReport;

        public UserReportController(IUserReportRepository userReport)
        {
            _userReport = userReport; 
        }

        [HttpPost]
        public async Task<IActionResult> ReportUser(UserReportModel model)
        {
            try
            {
                bool flag = await _userReport.ReportUser(model);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, flag));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
