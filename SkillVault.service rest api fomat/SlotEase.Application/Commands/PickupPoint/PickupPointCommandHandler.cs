using SlotEase.Application.DTO.PickupPoint;
using SlotEase.Domain.Entities.Locations;
using SlotEase.Domain.Interfaces;
using SlotEase.Infrastructure.Interfaces;
using System.Threading;

namespace SlotEase.Application.Commands.PickupPoint
{
    //##Update
    public class UpdatePickupPointCommandHandler : IRequestHandler<UpdatePickupPointCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<PickupPoints, long> _pickupPointRepository;
        private readonly IMapper _mapper;
        public UpdatePickupPointCommandHandler(IUnitOfWork unitOfWork, IRepository<PickupPoints, long> pickupPointRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _pickupPointRepository = pickupPointRepository ?? throw new ArgumentNullException(nameof(pickupPointRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Handle(UpdatePickupPointCommand request, CancellationToken cancellationToken)
        {
            PickupPointDto pickupPointList = request.PickupPointDto;
            try
            {
                PickupPoints existingPickupPoint = await _pickupPointRepository.GetAsync(pickupPointList.Id);
                if (pickupPointList != null)
                {
                    _mapper.Map(pickupPointList, existingPickupPoint);
                    _unitOfWork.BeginTransaction();
                    try
                    {
                        await _pickupPointRepository.UpdateAsync(existingPickupPoint);
                        _unitOfWork.SaveChanges();
                        _unitOfWork.CommitTransaction();
                        return true;

                    }
                    catch (DbUpdateException)
                    {
                        return false;
                    };
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
    //##DELETE
    public class DeletePickupPointCommandHandler : IRequestHandler<DeletePickupPointCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<PickupPoints, long> _pickupPointRepository;
        private readonly IMapper _mapper;
        public DeletePickupPointCommandHandler(IUnitOfWork unitOfWork, IRepository<PickupPoints, long> pickupPointRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _pickupPointRepository = pickupPointRepository ?? throw new ArgumentNullException(nameof(pickupPointRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Handle(DeletePickupPointCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                PickupPoints? pickup = _pickupPointRepository.GetAll().Where(x => x.Id == request.Id).FirstOrDefault();


                if (pickup != null)
                {

                    pickup.IsActive = false;
                    pickup.IsDeleted = true;
                    await _pickupPointRepository.UpdateAsync(pickup);
                    _unitOfWork.SaveChanges();
                    _unitOfWork.CommitTransaction();
                    return true;
                }
                _unitOfWork.RollbackTransaction();
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
