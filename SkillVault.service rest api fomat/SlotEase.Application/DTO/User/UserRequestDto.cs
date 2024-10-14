namespace SlotEase.Application.DTO.User
{
    public class UserRequestDto
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string email { get; set; } = string.Empty;

        public int UserType { get; set; }

    }
}
