

using SlotEase.Application.Commands.UserCommand;
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
public class UserController( IUser userQueries, IMediator mediator) : ControllerBase
{

    private readonly IUser _userQueries = userQueries ?? throw new ArgumentNullException(nameof(userQueries));
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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
    [AllowAnonymous]
    [HttpPost]

    public async Task<IActionResult> CreateUser(UserCreateDto UserCreateDto)
    {
        var obj_Res = new CreatedUserCommand(UserCreateDto);
        var result = await _mediator.Send(obj_Res);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("Update")]

    public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdateDto)
    {
        var obj_Res = new UpdatedUserCommand(userUpdateDto);
        var result = await _mediator.Send(obj_Res);
        return Ok(result);
    }



    [AllowAnonymous]
    [HttpDelete("Delete")]

    public async Task<IActionResult> DeleteUser(long Id)
    {
        var obj_Res = new DeletedUserCommand(Id);
        var result = await _mediator.Send(obj_Res);
        return Ok(result);
    }
}