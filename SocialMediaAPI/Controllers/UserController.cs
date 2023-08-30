using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPI.DataAccess.Users;
using SocialMediaAPI.Models;
using System.ComponentModel.DataAnnotations;
using SocialMediaAPI.DTOs.Users.GET.Responses;
using SocialMediaAPI.DTOs.Users.POST.Requests;
using SocialMediaAPI.DTOs.Users.PUT.Response;
using SocialMediaAPI.Validators;

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

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
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
        public ActionResult<UpdateUserResponse> GetUser([FromQuery] int id)
        {
            var response = _userRepository.GetUser(id);
            if (response is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UpdateUserResponse>(response));
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
        public ActionResult<UpdateUserResponse> Update([FromBody] User user)
        {
            var editedUser = _userRepository.Update(user);
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
        public ActionResult Delete([FromQuery] int id)
        {
            return _userRepository.Delete(id) ? Ok() : BadRequest();
        }
    }
}
