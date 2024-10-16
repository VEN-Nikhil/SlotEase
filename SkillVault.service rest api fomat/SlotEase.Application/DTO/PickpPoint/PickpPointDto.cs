using System.ComponentModel.DataAnnotations;

namespace SlotEase.Application.DTO.PickupPoint
{
    public class PickupPointDto
    {
        public long Id { get; set; }

        public string LocationName { get; set; } // Name of the location

        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90 degrees.")]
        public decimal Latitude { get; set; } // Latitude of the location

        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180 degrees.")]
        public decimal Longitude { get; set; } // Longitude of the location

        [Required(ErrorMessage = "Location type is required.")]
        [StringLength(100, ErrorMessage = "Location type can't be longer than 100 characters.")]
        public string LocationType { get; set; } // Type of the location

        [Required(ErrorMessage = "Pickup time is required.")]
        public TimeSpan PickupTime { get; set; } // This will store the standard expected time from the office

        [Range(0, double.MaxValue, ErrorMessage = "Distance from office must be a positive value.")]
        public decimal DistanceFromOffice { get; set; } // Distance from the office in kilometers/miles

        [Url(ErrorMessage = "Location icon must be a valid URL.")]
        public string LocationIcon { get; set; } = string.Empty; // URL or path to the location icon

        public bool IsActive { get; set; } = false; // Indicates if the location is active

        public bool IsVerified { get; set; } = false; // Indicates if the location is verified
    }
}
