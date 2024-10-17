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
        private readonly IMapper _mapper;

        public CreatedPickupPointCommandHandler(IUnitOfWork unitOfWork, IRepository<PickupPoints, long> pickupPointRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _pickupPointRepository = pickupPointRepository ?? throw new ArgumentNullException(nameof(pickupPointRepository));
            _mapper = mapper;
        }
        public async Task<bool> Handle(CreatedPickupPointCommand request, CancellationToken cancellationToken)
        {

            _unitOfWork.BeginTransaction();

            var data = _pickupPointRepository.GetAll().Where(x => request.PickupPointDto.LocationName.Contains(x.LocationName)).ToList();
            if (!data.Any())
            {
                PickupPoints pickupPoint = _mapper.Map<PickupPoints>(request.PickupPointDto);
                pickupPoint.LocationName = request.PickupPointDto.LocationName;
                pickupPoint.Latitude = request.PickupPointDto.Latitude;
                pickupPoint.Longitude = request.PickupPointDto.Longitude;
                pickupPoint.LocationType = request.PickupPointDto.LocationType;
                pickupPoint.PickupTime = request.PickupPointDto.PickupTime;
                pickupPoint.DistanceFromOffice = request.PickupPointDto.DistanceFromOffice;
                pickupPoint.LocationIcon = request.PickupPointDto.LocationIcon;
                pickupPoint.IsActive = request.PickupPointDto.IsActive;
                pickupPoint.IsVerified = request.PickupPointDto.IsVerified;
                pickupPoint.CreatorUserId = 0;
                await _pickupPointRepository.InsertAsync(pickupPoint);
            }
            _unitOfWork.SaveChanges();
            _unitOfWork.CommitTransaction();
            return (true);
        }
    }
}