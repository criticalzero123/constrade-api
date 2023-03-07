using ConstradeApi.Entity;
using ConstradeApi.Enums;
using ConstradeApi.Model.MCommunity;
using ConstradeApi.Model.MCommunity.MCommunityJoinRequest;
using ConstradeApi.Model.MCommunity.MCommunityPost;
using ConstradeApi.Model.MCommunity.MCommunityPostComment;
using ConstradeApi.Model.MCommunity.Repository;
using ConstradeApi.Model.MReport;
using ConstradeApi.Model.MReport.Repository;
using ConstradeApi.Model.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommunityController : ControllerBase
    {
        private readonly ICommunityRepository _communityRepo;
        private readonly IReportRepository _reportRepo;

        public CommunityController(ICommunityRepository communityRepo, IReportRepository report)
        {
            _communityRepo = communityRepo;
            _reportRepo = report;
        }

        [HttpPost("create")]
        public async Task<IActionResult> PostCreateCommunity([FromBody] CommunityModel info)
        {
            try
            {
                CommunityResponse response = await _communityRepo.CreateCommunity(info);

                if (response == CommunityResponse.NotVerified) return Ok(ResponseHandler.GetApiResponse(ResponseType.Failure, $"{response}"));

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, $"{response}"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex)); ;
            }
        }

        [HttpGet("created/{userId}")]
        public async Task<IActionResult> GetMyCommunity(int userId)
        {
            try
            {
                var list = await _communityRepo.GetCommunityByOwnerId(userId);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, list));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCommunity()
        {
            try
            {
                var list = await _communityRepo.GetCommunities();

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, list));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommunity(int id)
        {
            try
            {
                var model = await _communityRepo.GetCommunity(id);

                if (model == null) return NotFound("No Community Exist");

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommunity(int id, int userId)
        {
            try
            {
                bool deleted = await _communityRepo.DeleteCommunity(id, userId);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, deleted));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost("join")]
        public async Task<IActionResult> JoinCommunity([FromBody] CommunityJoinRequest info)
        {
            try
            {
                CommunityJoinResponse response = await _communityRepo.JoinCommunity(info.CommunityId, info.UserId);

                if (response == CommunityJoinResponse.Rejected) return Ok(ResponseHandler.GetApiResponse(ResponseType.Failure, $"{response}"));



                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, $"{response}"));


            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost("report")]
        public async Task<IActionResult> ReportCommunity([FromBody] ReportModel report)
        {
            try
            {
                bool flag = await _reportRepo.CreateReport(report);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, flag));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditCommunity([FromBody] CommunityModel info)
        {
            try
            {
                var flag = await _communityRepo.UpdateCommunity(info);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, flag));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }


        [HttpPost("{id}/post")]
        public async Task<IActionResult> PostCommunity([FromBody] CommunityPostModel info)
        {
            try
            {
                var result = await _communityRepo.CommunityCreatePost(info);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, result));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet("{id}/post")]
        public async Task<IActionResult> GetPostCommunity(int id)
        {
            try
            {
                var posts = await _communityRepo.GetAllCommunityPost(id);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success,posts));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost("{id}/post/report")]
        public async Task<IActionResult> ReportPostCommunity([FromBody] ReportModel model)
        {
            try
            {
                var report = await _reportRepo.CreateReport(model);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, report));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpDelete("{id}/post/{postId}")]
        public async Task<IActionResult> DeletePostCommunity(int postId, int userId)
        {
            try
            {
                bool deleted = await _communityRepo.DeletePostCommunityById(postId, userId);

                if (!deleted) return NotFound("You are not the owner or the post doesnt exist");

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, deleted));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
        
        [HttpPut("{id}/post/{postId}/like")]
        public async Task<IActionResult> LikePostCommunity(int postId, int userId)
        {
            try
            {
                bool flag = await _communityRepo.CommunityPostLike(postId, userId);

                if (!flag) return NotFound("No post found");

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, flag));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost("{id}/post/{postId}/comment")]
        public async Task<IActionResult> CommentPost([FromBody] CommunityPostCommentModel info)
        {
            try
            {
                var comment = await _communityRepo.CommentPost(info);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, comment));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet("{id}/post/{postId}/comment")]
        public async Task<IActionResult> GetCommentPost(int postId)
        {
            try
            {
                var comments = await _communityRepo.GetCommentByPostId(postId);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, comments));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost("{id}/post/{postId}/comment/report")]
        public async Task<IActionResult> ReportCommentPost([FromBody] ReportModel model)
        {
            try
            {
                var report = await _reportRepo.CreateReport(model);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, report));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpDelete("{id}/post/{postId}/comment/{commentId}")]
        public async Task<IActionResult> DeleteCommentPost(int commentId)
        {
            try
            {
                bool flag = await _communityRepo.DeleteCommentPost(commentId);

                if (!flag) return NotFound("Comment is not found or something went wrong");

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, flag));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
