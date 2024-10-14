using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SlotEase.Domain.Entities.Auditing;
using SlotEase.Domain.Entities.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlotEase.Domain.Entities.Driver
{
    public class Driver : FullAuditedEntity
    {
        [Required]
        [ForeignKey("User")]
        public long VendorId { get; set; }// From user->id
        [Required]
        [ForeignKey("User")]
        public long DriverId { get; set; }//From user->id
        public ICollection<User> Users { get; set; } //vendorId // driverId
    }
}