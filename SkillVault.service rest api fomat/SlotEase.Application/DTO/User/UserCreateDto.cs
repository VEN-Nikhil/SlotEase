using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotEase.Application.DTO.User
{
    public class UserCreateDto
    {
        public int Id { get; set; }
       
        public string Name { get; set; }
       
        public string LastName { get; set; }
     
        public string Email { get; set; }
        
        public string Gender { get; set; }
     
        public long PhoneNumber { get; set; }

      
        public string PhoneCode { get; set; }
        public int CompanyId { get; set; } = 0;
        public string Password { get; set; }=string.Empty;
        public DateTime? LastSignIn { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }
        public int UserDetailsId { get; set; }
        public DateTime CreationTime { get; set; }
        public int? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public int? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }

    }
}
