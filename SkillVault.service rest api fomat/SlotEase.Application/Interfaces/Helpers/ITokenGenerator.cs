namespace SlotEase.Application.Interfaces.Helpers;

public interface ITokenGenerator
{
    string GenerateJwtToken(long userId, out DateTime expiryTime);
}
