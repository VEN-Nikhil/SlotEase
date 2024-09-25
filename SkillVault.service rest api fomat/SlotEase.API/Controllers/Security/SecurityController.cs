using SlotEase.Application.DTO.Security;
using SlotEase.Application.Interfaces.Security;

namespace SlotEase.API.Controllers;
/// <summary>
/// This Controler Is For Handling The Login Functionality
/// </summary>
[Route(("api/v1.0/[controller]"))]
[ApiController]
[Authorize]
public class SecurityController(ILogger<SecurityController> logger, ISecurityQueries securityQueries, IMediator mediator) : ControllerBase
{
    private readonly ILogger<SecurityController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly ISecurityQueries _securityQueries = securityQueries ?? throw new ArgumentNullException(nameof(securityQueries));
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    /// <summary>
    /// This Method Is Used For Login Functionality
    /// </summary>
    /// <param name="login">login</param>
    /// <returns>It Returns User Details Along With Token.</returns>
    [AllowAnonymous]
    [HttpPost]
    [Route("authentication/login")]
    public async Task<IActionResult> Login([Required] LoginRequestDto login)
    {
        try
        {
            return Ok(await _securityQueries.SignInAsync(login));
        }
        catch (AppException ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }
}
