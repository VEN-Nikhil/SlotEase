using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SlotEase.Application.DTO.User
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Email { get; set; }
       // public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? LastSignIn { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }
   //     public int UserDetailsId { get; set; }
        public DateTime CreationTime { get; set; }
        // public int? CreatorUserId { get; set; }
        //  public DateTime? LastModificationTime { get; set; }
        //  public int? LastModifierUserId { get; set; }
        // public bool IsDeleted { get; set; }
        // public int? DeleterUserId { get; set; }
        // public DateTime? DeletionTime { get; set; }

        public string phoneNumber {  get; set; }
    }
}
