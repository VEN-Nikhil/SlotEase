using SlotEase.Application.DTO.PickupPoint;

namespace SlotEase.Application.Commands.PickupPoint
{
    public class UpdatePickupPointCommand : IRequest<bool>
    {
        public PickupPointDto PickupPointDto { get; set; }
        public UpdatePickupPointCommand(PickupPointDto pickupPointDto)
        {
            PickupPointDto = pickupPointDto;
        }

    }

    public class DeletePickupPointCommand : IRequest<bool>
    {
        public DeletePickupPointCommand(long id)
        {
            this.Id = id;
        }
        public long Id { get; set; }
    }
}
