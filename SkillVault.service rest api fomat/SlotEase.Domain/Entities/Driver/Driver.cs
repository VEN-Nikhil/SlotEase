using SlotEase.Domain.Entities.Auditing;
using SlotEase.Domain.Entities.Users;
using System.Collections.Generic;

namespace SlotEase.Domain.Entities.Driver
{
    public class Driver : FullAuditedEntity
    {
        public long vendorId { get; set; }// From user->id
        public long driverId { get; set; }//From user->id
        public ICollection<User> Users { get; set; } //vendorId // driverId
    }
}