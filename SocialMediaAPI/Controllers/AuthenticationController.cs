using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPI.DataAccess.Users;
using SocialMediaAPI.DTOs.Users.POST.Requests;
using SocialMediaAPI.DTOs.Users.POST.Responses;

namespace SocialMediaAPI.Controllers
{
    [ApiController]
    [Route("api/authenticator")]
    [Consumes("application/json")]
    [Produces("application/json", "application/xml")]
    public class AuthenticationController : ControllerBase 

    {
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public AuthenticationController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpPost("user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<UserLoginResponse> Login([FromBody] UserLoginRequest user)
    {
        var loggedInUser = _userRepository.LogIn(user.Email, user.Password);
        if (loggedInUser == null) return Unauthorized();
        return Ok(_mapper.Map<UserLoginResponse>(loggedInUser));
    }
    }
}
