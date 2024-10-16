using SlotEase.Application.DTO.PickpPoint;
using SlotEase.Application.DTO.PickupPoint;

namespace SlotEase.Application.Interfaces
{
    public interface IPickupPoint
    {
        Task<PickupPointDto> GetPickupPointByIdAsync(int id);
        Task<List<PickupPointDto>> GetAllPickupPoint(PickpPointRequestDto pickpPointRequest);
    }
}
