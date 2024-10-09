using SlotEase.Application.DTO.PickupPoint;
using SlotEase.Domain.Entities.Locations;

namespace SlotEase.Application.Mapping;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<PickupPointDto, PickupPoints>().ReverseMap();
    }
}