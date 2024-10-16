using FluentValidation;
using SlotEase.Application.DTO.PickupPoint;
using SlotEase.Application.Interfaces;

namespace SlotEase.Application.Validators
{
    public class LocationValidator : AbstractValidator<PickupPointDto>
    {
        private readonly IPickupPoint _locationService; // Injecting a service to check location names

        public LocationValidator(IPickupPoint pickupPoint)
        {
            _locationService = pickupPoint;

            RuleFor(x => x.LocationName)
                .NotEmpty().WithMessage("Location Name is required.")
                .Length(1, 100).WithMessage("Location Name must be between 1 and 100 characters.")
                .Must(BeUniqueLocationName).WithMessage("Location Name must be unique.");

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");

            RuleFor(x => x.LocationType)
                .NotEmpty().WithMessage("Location Type is required.");

            RuleFor(x => x.DistanceFromOffice)
                .GreaterThanOrEqualTo(0).WithMessage("Distance must be non-negative.");

            RuleFor(x => x.LocationIcon)
                .Must(x => Uri.IsWellFormedUriString(x, UriKind.Absolute)).WithMessage("Location Icon must be a valid URL.");
        }

        private bool BeUniqueLocationName(string locationName)
        {
            // Check if the location name already exists using the injected service
            return !_locationService.Equals(locationName);
        }
    }
}
