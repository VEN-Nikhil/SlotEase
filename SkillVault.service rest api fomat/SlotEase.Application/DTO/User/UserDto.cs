namespace SlotEase.Application.DTO.User
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? LastSignIn { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreationTime { get; set; }
        public string phoneNumber { get; set; }
        public int UserType { get; set; }
    }
}
