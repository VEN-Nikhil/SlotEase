namespace SlotEase.Application.DTO.Security;


public class LoginResponseDto
{
    public long Id { get; set; }
    public string UserName { get; set; }
    public string EmailAddress { get; set; }
    public bool IsActive { get; set; }
    public bool IsVerified { get; set; }
    public string Token { get; set; }
    public DateTime TokenExpiryTime { get; set; }
}
