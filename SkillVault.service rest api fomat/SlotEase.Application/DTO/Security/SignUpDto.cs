namespace SlotEase.Application.DTO.Security;

public class SignUpDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public long CellNumber { get; set; }
    public string EmailAddress { get; set; }
    public string UserId { get; set; }
    public string SocialSecurityNumber { get; set; }
    public string PreferredLanguage { get; set; }
    public string ProfileImage { get; set; }
    public string CountryCode { get; set; }
    public string PhoneCode { get; set; }
    public string Password { get; set; }

}

public class SignUpResponseDto
{
    public long UserId { get; set; }
}