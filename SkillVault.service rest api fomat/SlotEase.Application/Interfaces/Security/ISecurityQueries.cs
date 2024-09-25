using SlotEase.Application.DTO.Security;
using SlotEase.Helpers;

namespace SlotEase.Application.Interfaces.Security;

public interface ISecurityQueries
{
    Task<ApiResponse<LoginResponseDto>> SignInAsync(LoginRequestDto login);
}
