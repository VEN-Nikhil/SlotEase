using MediatR;
using SlotEase.Application.DTO;
using SlotEase.Application.DTO.User;

namespace SlotEase.Application.Commands.UserCommand
{
    // Sample command
    public class UpdatedUserCommand : IRequest<bool>
    {

      public UserCreateDto UserCreateDto { get; set; }

        public UpdatedUserCommand(UserCreateDto userCreateDto)
        {
          UserCreateDto = userCreateDto;
        }
    }
}
