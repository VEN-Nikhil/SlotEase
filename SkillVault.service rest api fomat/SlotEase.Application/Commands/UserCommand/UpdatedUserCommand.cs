using MediatR;
using SlotEase.Application.DTO;
using SlotEase.Application.DTO.User;

namespace SlotEase.Application.Commands.UserCommand
{
    public class UpdatedUserCommand : IRequest<bool>
    {
      public UserUpdateDto UserUpdateDto { get; set; }

        public UpdatedUserCommand(UserUpdateDto userUpdateDto)
        {
            UserUpdateDto = userUpdateDto;
        }
    }
}
