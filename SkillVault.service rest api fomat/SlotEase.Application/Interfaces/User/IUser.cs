using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlotEase.Application.DTO.User;

namespace SlotEase.Application.Interfaces.User
{
    public interface IUser
    {
        Task<List<UserDto>> GetUsersAsync(UserRequestDto userRequestDto);
        Task<UserDto> GetSingleUserAsync(int id);
    }
}
