using SlotEase.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlotEase.Domain.Entities.Locations
{
    [Table(nameof(PickupPoints))]
    public class PickupPoints : FullAuditedEntity<long>
    {
        [Required(ErrorMessage = "Location name is required.")]
        [StringLength(255, ErrorMessage = "Location name can't be longer than 255 characters.")]
        public string LocationName { get; set; }

        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90 degrees.")]
        public decimal Latitude { get; set; }

        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180 degrees.")]
        public decimal Longitude { get; set; }

        [Required(ErrorMessage = "Location type is required.")]
        [StringLength(100, ErrorMessage = "Location type can't be longer than 100 characters.")]
        public string LocationType { get; set; }

        [Required(ErrorMessage = "Pickup time is required.")]
        public TimeSpan PickupTime { get; set; }  // This will store the standard expected time from the office 

        [Range(0, double.MaxValue, ErrorMessage = "Distance from office must be a positive value.")]
        public decimal DistanceFromOffice { get; set; } // Distance from the office in kilometers/miles

        [Url(ErrorMessage = "Location icon must be a valid URL.")]
        public string LocationIcon { get; set; } = string.Empty;

        public bool IsActive { get; set; } = false;

        public bool IsVerified { get; set; } = false;
    }
}
