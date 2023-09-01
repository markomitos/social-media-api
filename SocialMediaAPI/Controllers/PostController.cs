using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPI.DataAccess.Posts;
using SocialMediaAPI.DataAccess.Users;
using SocialMediaAPI.DTOs.Posts.GET.Responses;
using SocialMediaAPI.DTOs.Posts.POST.Request;
using SocialMediaAPI.DTOs.Posts.PUT.Requests;
using SocialMediaAPI.DTOs.Posts.PUT.Response;
using SocialMediaAPI.DTOs.Users.PUT.Request;
using SocialMediaAPI.Models;

namespace SocialMediaAPI.Controllers
{
    [ApiController]
    [Route("api/Posts")]
    [Consumes("application/json")]
    [Produces("application/json", "application/xml")]
    public class PostController:ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public PostController(IPostRepository PostRepository, IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _postRepository = PostRepository;
            _mapper = mapper;
        }

        /// <summary>
        ///  Returns a list of Posts.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<GetPostResponse>> GetPosts()
        {
            var response = _postRepository.GetPosts();
            if (response.Count == 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<GetPostResponse>>(response));
        }

        /// <summary>
        /// Returns a Post by its id
        /// </summary>
        /// <param name="id">id of the Post</param>
        /// <returns>Post with the given id or not found</returns>
        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UpdatePostResponse> GetPost([FromQuery] int id)
        {
            var response = _postRepository.GetPost(id);
            if (response is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UpdatePostResponse>(response));
        }

        /// <summary>
        /// Returns Posts with given tag
        /// </summary>
        /// <param name="tag">tag of the Post</param>
        /// <returns>Posts with the given tag or not found</returns>
        [HttpGet("tag")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UpdatePostResponse> GetPostByTag([FromQuery] string tag)
        {
            var response = _postRepository.GetPostsByTag(tag);
            if (response.Count == 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<GetPostResponse>>(response));
        }

        /// <summary>
        /// Returns Posts with given user
        /// </summary>
        /// <param name="userId">Id of the User</param>
        /// <returns>Posts from the given User or not found</returns>
        [HttpGet("user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UpdatePostResponse> GetPostByUser([FromQuery] int userId)
        {
            var response = _postRepository.GetPostsByUser(userId);
            if (response.Count == 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<GetPostResponse>>(response));
        }

        /// <summary>
        /// Create a Post
        /// </summary>
        /// <param name="post">The Post to be crated</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CreatePost([FromBody] CreatePostRequest post)
        {
            if (Validators.Validator.IsValid(post))
            {
                var createdPost = _postRepository.CreatePost(_mapper.Map<Post>(post));
                if (createdPost is not null)
                {
                    return Created($"/Posts/{createdPost.Id}", createdPost);
                }
            }

            return BadRequest();

        }

        /// <summary>
        /// Allowed options on the Post resource
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,PUT,DELETE");
            return Ok();
        }

        /// <summary>
        /// Updates a given Post
        /// </summary>
        /// <param name="Post"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UpdatePostResponse> Update([FromBody] UpdatePostRequest post)
        {
            var editedPost = _postRepository.UpdatePost(_mapper.Map<Post>(post));
            return editedPost is not null
                ? Ok(_mapper.Map<UpdatePostResponse>(editedPost))
                : BadRequest(_mapper.Map<UpdatePostResponse>(post));
        }

        /// <summary>
        /// Physically deletes a Post with a given id 
        /// </summary>
        /// <param name="id">id of the Post to be deleted</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete([FromQuery] int id)
        {
            return _postRepository.DeletePost(id) ? Ok() : BadRequest();
        }

        [HttpGet("popular")]
        public ActionResult<List<GetPostResponse>> GetMostPopularPosts([FromQuery] int num)
        {
            var response = _postRepository.GetMostPopularPosts(num);
            if (response.Count == 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<GetPostResponse>>(response));
        }

        [HttpPatch("like")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Like([FromQuery] int postId, [FromQuery] int userId)
        {
            try
            {
                return _postRepository.LikePost(postId, _userRepository.GetUser(userId)) ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }

        }

        [HttpPatch("archive")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult ArchivePost([FromQuery] int id)
        {
            return _postRepository.ArchivePost(id) ? Ok() : BadRequest();
        }

        [HttpPatch("activate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult ActivatePost([FromQuery] int id)
        {
            return _postRepository.ActivatePost(id) ? Ok() : BadRequest();
        }
    }
}
