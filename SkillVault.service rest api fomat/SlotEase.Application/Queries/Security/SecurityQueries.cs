using SlotEase.Application.DTO.Security;
using SlotEase.Application.Interfaces.Security;
using SlotEase.Domain.Entities.Users;
using SlotEase.Domain.Helpers;
using SlotEase.Domain.Interfaces;
using SlotEase.Helpers;
using SlotEase.Application.Helpers;
using SlotEase.Application.Interfaces.Helpers;

namespace SlotEase.Application.Queries.Security;

public class SecurityQueries(IRepository<User, long> userRepository, ITokenGenerator tokenGenerator,
                             IPasswordHasher passwordHasher, IMapper mapper) : ISecurityQueries
{
    private readonly IRepository<User, long> _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly ITokenGenerator _tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
    private readonly IPasswordHasher _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    public async Task<ApiResponse<LoginResponseDto>> SignInAsync(LoginRequestDto login)
    {
        string encryptedPassword = _passwordHasher.HashPassword(login.Password);
        User signInUser = await _userRepository.GetAll()
            .Where(u => u.Email.ToLower() == login.UserName.ToLower() && u.Password == encryptedPassword && u.IsActive)
            .FirstOrDefaultAsync();
        if (signInUser == null)
        {
            return ApiResponseHelper.BadRequestResponse<LoginResponseDto>("Invalid username or password");
        }

        string token = _tokenGenerator.GenerateJwtToken(userId: signInUser.Id, out DateTime expiryTime);

        LoginResponseDto responseData = new()
        {
            Id = signInUser.Id,
            UserName = login.UserName,
            EmailAddress = signInUser.Email,
            IsActive = signInUser.IsActive,
            IsVerified = signInUser.IsVerified,
            Token = token,
            TokenExpiryTime = expiryTime,
        };
        return ApiResponseHelper.OkResponse(responseData, "Success");
    }

}

public interface IPasswordHasher
{
    string HashPassword(string password);
}
public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return CryptographyHelper.Encrypt(password);
    }
}
