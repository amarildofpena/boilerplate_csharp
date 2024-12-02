using Business.Interfaces;
using Business.Model.Users;
using Business.RequestModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rest.RequestModels.Users;

namespace API.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        [Route("Search")]
        public ActionResult<IEnumerable<User>> BuscarUsuarios([FromQuery]SearchUserPayloadModel request)
        {
            try
            {
                return Ok( _userService.buscarUsuarios(request.Nome, request.Email));
            }catch(Exception e)
            {
                return StatusCode(500, $"Falha ao realizar consulta {e.Message}");
            }
        }

        [Authorize]
        [HttpPost("CreateUser")]
        public ActionResult<CreateUserResponseModel> CreateUser([FromBody] CreateUserPayloadModel payload)
        {
            try
            {
                var response = _userService.CreateUser(payload);

                if (response.StatusCode == 200)
                {
                    return Ok(response);
                }

                return StatusCode(response.StatusCode, response);

            }
            catch(Exception e)
            {
                return StatusCode(500, $"Falha ao realizar consulta {e.Message}");

            }
            
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] TryLoginPayloadModel loginDto)
        {
            var response = _userService.Authenticate(loginDto.Email, loginDto.Password);

            if (response.StatusCode == 200)
            {
                return Ok(response);
            }

            return StatusCode(response.StatusCode, response);
        }


    }
}
