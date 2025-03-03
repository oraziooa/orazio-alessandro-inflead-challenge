using Challange.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Challange.Data.DTO;

namespace Challange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UsersController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUser([FromQuery] string? gender, [FromQuery] string? email, [FromQuery] string? username)
        {
            var user = await _userRepository.GetUserByFilterAsync(gender, email, username);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var userDTO = new UserDTO
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                Username = user.Username,
                FullName = user.Fullname,
                ProfilePicUrl = user.Profilepicurl,
                Gender = user.Gender,
                PhoneNumber = user.Phonenumber,
                Employment = user.Employment,
                KeySkill = user.Keyskill,
                AddressId = user.Addressid?.ToString(),
                Address = user.Address != null ? new AddressDTO
                {
                    City = user.Address.City,
                    State = user.Address.State,
                    Street = user.Address.Street,
                    Zipcode = user.Address.Zipcode
                } : null,
                CreationDate = user.Creationdate ?? DateTime.UtcNow
            };

            return Ok(userDTO);
        }

    }
}
