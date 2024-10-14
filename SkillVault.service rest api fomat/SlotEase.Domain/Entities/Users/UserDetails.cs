using SlotEase.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace SlotEase.Domain.Entities.Users;

public class UserDetails : FullAuditedEntity
{
    [Required]
    public string phoneNumber { get; set; }
    public string profileImage { get; set; }
    [Required]
    public int companyId { get; set; }
    [Required]
    public bool IsActive { get; set; } = false;
    [Required]
    public bool IsVerified { get; set; } = false;
    [Required]
    [ForeignKey("User")]
    public long userId { get; set; }
    public ICollection<User> Users { get; set; }
}
