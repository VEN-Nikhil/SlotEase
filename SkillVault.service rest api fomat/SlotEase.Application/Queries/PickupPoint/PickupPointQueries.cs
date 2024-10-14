using SlotEase.Application.DTO.PickpPoint;
using SlotEase.Application.DTO.PickupPoint;
using SlotEase.Application.Interfaces;
using SlotEase.Domain.Entities.Locations;
using SlotEase.Domain.Interfaces;




namespace SlotEase.Application.Queries.PickupPoint
{
    public class PickupPointQueries : IPickupPoint
    {
        public readonly IRepository<PickupPoints, long> _pickupPointRepository;
        public PickupPointQueries(IRepository<PickupPoints, long> pickupPointRepository)
        {
            _pickupPointRepository = pickupPointRepository ?? throw new ArgumentNullException(nameof(pickupPointRepository));
        }
        public async Task<List<PickupPointDto>> GetAllPickupPoint(PickpPointRequestDto pickpPointRequest)
        {
            try
            {
                var pickupPoints = await _pickupPointRepository.GetAll()
                    .Select(pickupPoint => new PickupPointDto
                    {
                        Id = pickupPoint.Id,
                        LocationName = pickupPoint.LocationName,
                        Latitude = pickupPoint.Latitude,
                        Longitude = pickupPoint.Longitude,
                        LocationType = pickupPoint.LocationType,
                        PickupTime = pickupPoint.PickupTime,
                        DistanceFromOffice = pickupPoint.DistanceFromOffice,
                        LocationIcon = pickupPoint.LocationIcon,
                        IsActive = pickupPoint.IsActive,
                        IsVerified = pickupPoint.IsVerified
                    }).ToListAsync();
                return pickupPoints;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the pickup points.", ex);
            }
        }
        public async Task<PickupPointDto> GetPickupPointByIdAsync(int id)
        {
            try
            {
                var pickupPoint = await _pickupPointRepository.GetAll()
                    .Where(x => x.Id == id)
                    .Select(pickupPoint => new PickupPointDto
                    {
                        Id = pickupPoint.Id,
                        LocationName = pickupPoint.LocationName,
                        Latitude = pickupPoint.Latitude,
                        Longitude = pickupPoint.Longitude,
                        LocationType = pickupPoint.LocationType,
                        PickupTime = pickupPoint.PickupTime,
                        DistanceFromOffice = pickupPoint.DistanceFromOffice,
                        LocationIcon = pickupPoint.LocationIcon,
                        IsActive = pickupPoint.IsActive,
                        IsVerified = pickupPoint.IsVerified
                    })
                    .FirstOrDefaultAsync();
                return pickupPoint;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the pickup point.", ex);
            }
        }
    }
}
