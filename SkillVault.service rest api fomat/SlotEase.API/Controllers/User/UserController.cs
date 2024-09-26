

using SlotEase.Application.DTO.Security;
using SlotEase.Application.DTO.User;
using SlotEase.Application.Interfaces.Security;
using SlotEase.Application.Interfaces.User;
using System.Collections.Generic;

namespace SlotEase.API.Controllers;
/// <summary>
/// This Controler Is For Handling The Login Functionality
/// </summary>
[Route(("api/v1.0/[controller]"))]
[ApiController]
[Authorize]
public class UserController( IUser userQueries) : ControllerBase
{

    private readonly IUser _userQueries = userQueries ?? throw new ArgumentNullException(nameof(userQueries));
    [AllowAnonymous]
    [HttpPost]
    [Route("Users")]
    public async Task<IActionResult> GetUser(UserRequestDto userRequestDto)
    {
        try
        {
            List<UserDto> users = await userQueries.GetUsersAsync(userRequestDto);
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while retrieving the users.");
        }
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetSingleUser(int id)
    {
        try
        {
            UserDto user = await userQueries.GetSingleUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        catch (Exception ex)
        {

            return StatusCode(500, "An error occurred while retrieving the user.");
        }
    }

}



