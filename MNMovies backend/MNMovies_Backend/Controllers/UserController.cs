using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MNMovies_Backend.Data;
using MNMovies_Backend.Models;
using MNMovies_Backend.Services;

namespace MNMovies_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly JwtTokenService _jwtTokenService;

        public UserController(UserRepository userRepository, JwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.SelectAll();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetUserByID(int id)
        {
            var user = _userRepository.SelectByPK(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [Authorize]
        [HttpPost]
        public IActionResult InsertUser([FromBody] UserModel userModel)
        {
            if (userModel == null)
            {
                return BadRequest();
            }
            bool IsInserted = _userRepository.Insert(userModel);
            if (IsInserted)
            {
                return Ok(new { message = "User Insterted Successfully" });
            }
            return StatusCode(500, "An Error Occurred while Instert The User");
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserModel userModel)
        {
            if (userModel == null || id != userModel.UserID)
            {
                return BadRequest();
            }
            var IsUpdated = _userRepository.Update(userModel);
            if (!IsUpdated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var isDelete = _userRepository.Delete(id);
            if (!isDelete)
            {
                return NotFound();
            }
            return NoContent();
        }

        //[HttpPost("login")]
        //public IActionResult Login([FromBody] UserLoginModel loginModel)
        //{
        //    if (loginModel == null)
        //    {
        //        return BadRequest();
        //    }
        //    var user = _userRepository.Login(loginModel.UserName, loginModel.Password);
        //    if (user == null)
        //    {
        //        return Unauthorized(new { message = "Invalid username or password." });
        //    }
        //    return Ok(user);
        //}

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest(new { message = "Invalid request." });
            }

            var user = _userRepository.Login(loginModel.UserName, loginModel.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }

            var token = _jwtTokenService.GenerateToken(user);

            return Ok(new
            {
                token,
                user = new
                {
                    user.UserID,
                    user.UserName,
                    user.Email,
                    user.MobileNo,
                    user.Role
                }
            });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterModel registerModel)
        {
            if (registerModel == null)
            {
                return BadRequest();
            }
            var isRegistered = _userRepository.Register(registerModel);
            if (!isRegistered)
            {
                return StatusCode(500, "An error occurred while registering the user.");
            }
            return Ok(new { message = "User registered successfully." });
        }
    }
}
