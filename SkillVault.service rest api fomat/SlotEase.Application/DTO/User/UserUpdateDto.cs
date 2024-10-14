using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotEase.Application.DTO.User
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name can't be longer than 50 characters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name can't be longer than 50 characters")]
        public string Lastname { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        [StringLength(10, ErrorMessage = "Gender can't be longer than 10 characters")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\+\d{1,3}\d{4,14}$", ErrorMessage = "Phone number must be in the format +[country code][number]")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Company ID is required")]
        public int CompanyId { get; set; } = 0;

        public string Password { get; set; } = string.Empty;
        public DateTime? LastSignIn { get; set; }
        [Required(ErrorMessage = "Active status is required")]
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }
        [Required(ErrorMessage = "User details ID is required")]
        public int UserDetailsId { get; set; }
        [Required(ErrorMessage = "Creation time is required")]
        public DateTime CreationTime { get; set; }
        public int? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public int? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
    }
}
