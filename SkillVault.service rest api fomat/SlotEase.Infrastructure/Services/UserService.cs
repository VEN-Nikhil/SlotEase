using Microsoft.AspNetCore.Http;
using SlotEase.Domain.Interfaces;
using System.Linq;
using System.Security.Claims;


namespace SlotEase.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public long GetUserId()
    {
        if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null)
        {
            ClaimsIdentity claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                Claim userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    return 0;// Convert.ToInt64(userIdClaim.Value);
                }
            }
        }
        return 0;
    }




    public string GetToken()
    {
        if (_httpContextAccessor?.HttpContext?.Request != null && _httpContextAccessor?.HttpContext.Request.Headers != null)
        {
            return _httpContextAccessor?.HttpContext.Request.Headers["Authorization"];

        }
        return null;

    }
}
