using System.Security.Claims;
using Backend_Teamwork.src.Services.user;
using Backend_Teamwork.src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Backend_Teamwork.src.DTO.UserDTO;
using static Backend_Teamwork.src.Entities.User;

namespace Backend_Teamwork.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        // DI
        public UsersController(IUserService service)
        {
            _userService = service;
        }

        // GET: api/v1/users
        [HttpGet]
        [Authorize(Roles = "Admin")] // Accessible by Admin
        public async Task<ActionResult<List<UserReadDto>>> GetUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")] // Only Admin
        public async Task<ActionResult<UserReadDto>> GetUserById([FromRoute] Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }

        [HttpGet("profile/{id}")]
        public async Task<ActionResult<UserReadDto>> GetInformationById([FromRoute] Guid id)
        {
            // Get the user ID from the token claims
            var authClaims = HttpContext.User;
            var userId = authClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var convertedUserId = new Guid(userId);
            var user = await _userService.GetByIdAsync(id, convertedUserId);
            return Ok(user);
        }

        // GET: api/v1/users/email
        [HttpGet("email")]
        [Authorize(Roles = "Admin")] // Only Admin
        public async Task<ActionResult<UserReadDto>> GetByEmail([FromRoute] string email)
        {
            var user = await _userService.GetByEmailAsync(email);
            return Ok(user);
        }

        // POST: api/v1/users
        [HttpPost]
        public async Task<ActionResult<UserReadDto>> SignUp([FromBody] UserCreateDto createDto)
        {
            var UserCreated = await _userService.CreateOneAsync(createDto);
            return CreatedAtAction(nameof(GetUserById), new { id = UserCreated.Id }, UserCreated);
        }

        // POST: api/v1/users/create-admin
        [HttpPost("create-admin")]
        [Authorize(Roles = "Admin")] // Only Admin
        public async Task<ActionResult<UserReadDto>> CreateAdmin([FromBody] UserCreateDto createDto)
        {
            createDto.Role = UserRole.Admin; // Set role as 'Admin'
            var adminCreated = await _userService.CreateOneAsync(createDto);
            return CreatedAtAction(nameof(GetUserById), new { id = adminCreated.Id }, adminCreated);
        }

        // POST: api/v1/users/signin
        [HttpPost("signin")]
        public async Task<ActionResult<string>> SignIn([FromBody] UserCreateDto createDto)
        {
            var token = await _userService.SignInAsync(createDto);
            return Ok(token);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> UpdateUser(
            [FromRoute] Guid id,
            [FromBody] UserUpdateDto updateDto
        )
        {
            await _userService.UpdateOneAsync(id, updateDto);
            return NoContent();
        } // should ask my teammates

        [HttpPut("profile/{id}")]
        public async Task<ActionResult<bool>> UpdateProfileInformation(
            [FromRoute] Guid id,
            [FromBody] UserUpdateDto updateDto
        )
        {
            // Get the user ID from the token claims
            var authClaims = HttpContext.User;
            var userId = authClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var convertedUserId = new Guid(userId);
            await _userService.UpdateOneAsync(id, convertedUserId, updateDto);
            return NoContent();
        } // should ask my teammates

        // DELETE: api/v1/users/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Only Admin
        public async Task<ActionResult<bool>> DeleteUser([FromRoute] Guid id)
        {
            await _userService.DeleteOneAsync(id);
            return NoContent();
        }

        // Extra Features

        // search-by-phone-number
        [HttpGet("search-by-phone/{phoneNumber}")]
        [Authorize(Roles = "Admin")] // Only Admin
        public async Task<ActionResult<UserReadDto>> GetByPhone([FromRoute] string phoneNumber)
        {
            var user = await _userService.GetByPhoneNumberAsync(phoneNumber);
            return Ok(user);
        }

        // GET: api/v1/users/page
        [HttpGet("pagination")]
        [Authorize(Roles = "Admin")] // Only Admin
        public async Task<ActionResult<UserReadDto>> GetUsersByPage(
            [FromQuery] PaginationOptions paginationOptions
        )
        {
            var users = await _userService.GetUsersByPage(paginationOptions);
            return Ok(users);
        }

        // GET: api/v1/users/count
        [HttpGet("count")]
        [Authorize(Roles = "Admin")] // Only Admin
        public async Task<ActionResult<int>> GetTotalUsersCount()
        {
            var count = await _userService.GetTotalUsersCountAsync();
            return Ok(count);
        }
    }
}
