using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperNumberProject.Services;

namespace SuperNumberProject.Controllers.API
{
    [Route("api/home")]
    [ApiController]
    public class HomeApiController : ControllerBase
    {
        private readonly UserServices _userServices;
        private readonly RegistroServices _registroServices;

        public HomeApiController(UserServices userServices, RegistroServices registroServices)
        {
            _userServices = userServices;
            _registroServices = registroServices;
        }

        [HttpPost("authenticate")]
        public IActionResult AuthenticateUser([FromBody] UserRequest request)
        {
            try
            {
                var user = _userServices.AuthenticateUser(request.Name, request.Password);
                if (user != null)
                {
                    return Ok(user);
                }
                return Unauthorized("Invalid credentials.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    
        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] UserRequest request)
        {
            try
            {
                var user = _registroServices.CreateUser(request.Name, request.Password);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class UserRequest
        {
            public string Name { get; set; }
            public string Password { get; set; }
        }
    }
}
