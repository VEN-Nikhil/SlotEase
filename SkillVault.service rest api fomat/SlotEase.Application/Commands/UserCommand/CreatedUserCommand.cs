using SlotEase.Application.DTO.User;

namespace SlotEase.Application.Commands.UserCommand
{
    // Sample command
    public class CreatedUserCommand : IRequest<string>
    {
        public UserCreateDto UserCreateDto { get; set; }
        public CreatedUserCommand(UserCreateDto userCreateDto)
        {
            UserCreateDto = userCreateDto;
        }
    }

}
