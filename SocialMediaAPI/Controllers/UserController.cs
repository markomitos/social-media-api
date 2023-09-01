using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPI.DataAccess.Users;
using SocialMediaAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Windows.Markup;
using SocialMediaAPI.DTOs.Users.GET.Responses;
using SocialMediaAPI.DTOs.Users.POST.Requests;
using SocialMediaAPI.DTOs.Users.PUT.Request;
using SocialMediaAPI.DTOs.Users.PUT.Response;
using SocialMediaAPI.Validators;
using Microsoft.AspNetCore.Routing;
using SocialMediaAPI.HATEOAS;

namespace SocialMediaAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Consumes("application/json")]
    [Produces("application/json", "application/xml")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private const string controllerName = "User";  

        public UserController(IUserRepository userRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        /// <summary>
        ///  Returns a list of users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<GetUserResponse>> GetUsers()
        {
            var response = _userRepository.GetUsers();
            if (response.Count == 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<GetUserResponse>>(response));
        }

        /// <summary>
        /// Returns a user by its id
        /// </summary>
        /// <param name="id">id of the user</param>
        /// <returns>user with the given id or not found</returns>
        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<GetUserResponse> GetUser([FromQuery] int id)
        {
            var response = _userRepository.GetUser(id);
            if (response is null)
            {
                return NotFound();
            }

            return Ok(GenerateUserLinks(_mapper.Map<GetUserResponse>(response)));
        }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="user">The user to be crated</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CreateUser([FromBody] CreateUserRequest user)
        {
            if (Validators.Validator.IsValid(user))
            {
                var createdUser = _userRepository.CreateUser(_mapper.Map<User>(user));
                if (createdUser is not null)
                {
                    return Created($"/users/{createdUser.Id}", createdUser);
                }
            }

            return BadRequest();

        }

        /// <summary>
        /// Allowed options on the user resource
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
        /// Updates a given user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UpdateUserResponse> UpdateUser([FromBody] UpdateUserRequest user)
        {
            var editedUser = _userRepository.Update(_mapper.Map<User>(user));
            return editedUser is not null
                ? Ok(_mapper.Map<UpdateUserResponse>(user))
                : BadRequest(_mapper.Map<UpdateUserResponse>(user));
        }

        /// <summary>
        /// Physically deletes a user with a given id 
        /// </summary>
        /// <param name="id">id of the user to be deleted</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteUser([FromQuery] int id)
        {
            return _userRepository.Delete(id) ? Ok() : BadRequest();
        }

        [HttpPatch("deactivateId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Deactivate([FromQuery]int id)
        {
            return _userRepository.DeactivateAccount(id)? Ok(): BadRequest();
        }

        [HttpPatch("activateId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Activate(int id)
        {
            return _userRepository.ActivateAccount(id) ? Ok() : BadRequest();
        }

        [HttpGet("name")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UpdateUserResponse> GetUser([FromQuery] string firstName,[FromQuery] string lastName)
        {
            var response = _userRepository.GetUser(firstName,lastName);
            if (response is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UpdateUserResponse>(response));
        }

        [HttpPatch("follow")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Follow([FromQuery] int followerId, [FromQuery] int followedId)
        {
            try
            {
                return _userRepository.FollowUser(followerId, followedId) ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
             
        }

        private GetUserResponse GenerateUserLinks(GetUserResponse response)
        {
            var Links = new List<Link>()
            {
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetUsers)), controllerName, "GET"),
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetUser), values: new {id= response.Id}), controllerName, "GET"),
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(CreateUser)), controllerName, "POST"),
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(UpdateUser)), controllerName, "PUT"),
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(Follow), values: new { followerId = response.Id, followedId = response.Id }), controllerName, "PATCH"),
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(DeleteUser), values: new { id = response.Id }), controllerName, "DELETE"),
            };
            response.Links = Links;
            return response;
        }
    }
}
