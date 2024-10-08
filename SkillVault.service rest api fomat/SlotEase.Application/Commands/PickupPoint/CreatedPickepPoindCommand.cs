using SlotEase.Application.DTO.PickupPoint;
namespace SlotEase.Application.Commands.PickupPoint
{
    // Sample command
    public class CreatedPickupPointCommand : IRequest<bool>
    {
        public PickupPointDto PickupPointDto { get; set; }
        public CreatedPickupPointCommand(PickupPointDto pickupPointDto)
        {
            PickupPointDto = pickupPointDto;
        }
    }
}