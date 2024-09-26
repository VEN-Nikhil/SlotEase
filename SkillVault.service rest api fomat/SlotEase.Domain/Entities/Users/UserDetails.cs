using SlotEase.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlotEase.Domain.Entities.Users;

public class UserDetails : FullAuditedEntity
{
    public long PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime LastSignIn { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneCode { get; set; }
    public string Gender { get; set; } = string.Empty;
    public int UserType { get; set; }
    public string ProfileImage { get; set; }
    public string CompanyId { get; set; }
    public bool IsActive { get; set; }
    public bool IsVerified { get; set; }
    [ForeignKey("User")]
    public long UserId { get; set; }

    public ICollection<User> Users { get; set; }

}
