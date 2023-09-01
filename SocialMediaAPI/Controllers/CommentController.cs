using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPI.DataAccess.Comments;
using SocialMediaAPI.DTOs.Comments.GET.Responses;
using SocialMediaAPI.DTOs.Comments.POST.Requests;
using SocialMediaAPI.DTOs.Comments.PUT.Requests;
using SocialMediaAPI.DTOs.Comments.PUT.Responses;
using SocialMediaAPI.Models;

namespace SocialMediaAPI.Controllers
{
    [ApiController]
    [Route("api/Comments")]
    [Consumes("application/json")]
    [Produces("application/json", "application/xml")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _CommentRepository;
        private readonly IMapper _mapper;

        public CommentController(ICommentRepository CommentRepository, IMapper mapper)
        {
            _CommentRepository = CommentRepository;
            _mapper = mapper;
        }

        /// <summary>
        ///  Returns a list of Comments.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<GetCommentResponse>> GetComments()
        {
            var response = _CommentRepository.GetComments();
            if (response.Count == 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<GetCommentResponse>>(response));
        }

        /// <summary>
        /// Returns a Comment by its id
        /// </summary>
        /// <param name="id">id of the Comment</param>
        /// <returns>Comment with the given id or not found</returns>
        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UpdateCommentResponse> GetComment([FromQuery] int id)
        {
            var response = _CommentRepository.GetComment(id);
            if (response is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UpdateCommentResponse>(response));
        }

        /// <summary>
        /// Create a Comment
        /// </summary>
        /// <param name="comment">The Comment to be crated</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CreateComment([FromBody] CreateCommentRequest comment)
        {
            if (Validators.Validator.IsValid(comment))
            {
                var createdComment = _CommentRepository.CreateComment(_mapper.Map<Comment>(comment));
                if (createdComment is not null)
                {
                    return Created($"/Comments/{createdComment.Id}", createdComment);
                }
            }

            return BadRequest();

        }

        /// <summary>
        /// Allowed options on the Comment resource
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetOptions()
        {
            Response.Headers.Add("Allow", "GET,Comment,PUT,DELETE");
            return Ok();
        }

        /// <summary>
        /// Updates a given Comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UpdateCommentResponse> Update([FromBody] UpdateCommentRequest comment)
        {
            var editedComment = _CommentRepository.UpdateComment(_mapper.Map<Comment>(comment));
            return editedComment is not null
                ? Ok(_mapper.Map<UpdateCommentResponse>(comment))
                : BadRequest(_mapper.Map<UpdateCommentResponse>(comment));
        }

        /// <summary>
        /// Physically deletes a Comment with a given id 
        /// </summary>
        /// <param name="id">id of the Comment to be deleted</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete([FromQuery] int id)
        {
            return _CommentRepository.DeleteComment(id) ? Ok() : BadRequest();
        }
    }
}
