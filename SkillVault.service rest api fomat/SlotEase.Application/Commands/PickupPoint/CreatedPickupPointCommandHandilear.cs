using SlotEase.Application.Commands.PickupPoint;
using SlotEase.Domain.Entities.Locations;
using SlotEase.Domain.Interfaces;
using SlotEase.Infrastructure.Interfaces;
using System.Threading;
namespace SlotEase.Application.Commands.PickupPointCommand
{
    public class CreatedPickupPointCommandHandler : IRequestHandler<CreatedPickupPointCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<PickupPoints, long> _pickupPointRepository;

        public CreatedPickupPointCommandHandler(IUnitOfWork unitOfWork, IRepository<PickupPoints, long> pickupPointRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _pickupPointRepository = pickupPointRepository ?? throw new ArgumentNullException(nameof(pickupPointRepository));
        }
        public async Task<bool> Handle(CreatedPickupPointCommand request, CancellationToken cancellationToken)
        {
            var data = _pickupPointRepository.GetAll().Where(x => request.PickupPointDto.LocationName.Contains(x.LocationName)).ToList();
            if (!data.Any())
            {
                var pickupPoint = new PickupPoints
                {
                    LocationName = request.PickupPointDto.LocationName,
                    Latitude = request.PickupPointDto.Latitude,
                    Longitude = request.PickupPointDto.Longitude,
                    LocationType = request.PickupPointDto.LocationType,
                    PickupTime = request.PickupPointDto.PickupTime,
                    DistanceFromOffice = request.PickupPointDto.DistanceFromOffice,
                    LocationIcon = request.PickupPointDto.LocationIcon,
                    IsActive = request.PickupPointDto.IsActive,
                    IsVerified = request.PickupPointDto.IsVerified,
                    CreatorUserId = 0,
                    CreationTime = DateTime.UtcNow,
                    LastModificationTime = DateTime.UtcNow,
                };
                _pickupPointRepository.Insert(pickupPoint);
            }

            _unitOfWork.SaveChanges();

            return await Task.FromResult(true);
        }
    }
}