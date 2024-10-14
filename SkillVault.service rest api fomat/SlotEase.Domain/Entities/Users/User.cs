using SlotEase.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlotEase.Domain.Entities.Users;

[Table(nameof(User))]
public class User : FullAuditedEntity<long>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; } = string.Empty;
    public int UserType { get; set; }
    public DateTime LastSignIn { get; set; }
    public bool IsActive { get; set; }
    public bool IsVerified { get; set; }

}
