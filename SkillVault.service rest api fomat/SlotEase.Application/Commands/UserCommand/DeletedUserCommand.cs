using MediatR;
using SlotEase.Application.DTO;
using SlotEase.Application.DTO.User;

namespace SlotEase.Application.Commands.UserCommand
{
    // Sample command
    public class DeletedUserCommand : IRequest<bool>
    {

      public long Id{ get; set; }

        public DeletedUserCommand(long  id)
        {
          Id = id;
        }
    }
}
