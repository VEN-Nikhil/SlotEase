using SlotEase.Application.DTO.User;

namespace SlotEase.Application.Interfaces.User
{
    public interface IUser
    {
        Task<List<UserDto>> GetUsersAsync(UserRequestDto userRequestDto);
        Task<UserDto> GetSingleUserAsync(int id);
    }
}
