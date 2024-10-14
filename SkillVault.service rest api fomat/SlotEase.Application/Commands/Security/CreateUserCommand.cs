using SlotEase.Application.DTO.Security;
using SlotEase.Helpers;

namespace SlotEase.Application.Commands;

public class CreatedUserCommand : IRequest<ApiResponse<SignUpResponseDto>>
{
    public CreatedUserCommand(SignUpDto signUpDto)
    {
        this.signUpDto = signUpDto ?? throw new ArgumentNullException(nameof(signUpDto));

    }

    public SignUpDto signUpDto { get; private set; }


}

