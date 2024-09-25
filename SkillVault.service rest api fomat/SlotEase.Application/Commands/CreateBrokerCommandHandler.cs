using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using SlotEase.Infrastructure.Interfaces;
using SlotEase.Application.DTO;

namespace SlotEase.Application.Commands;

// Sample command handler
public class CreateBrokerCommandCommandHandler : IRequestHandler<CreateBrokerCommand, BrokerDto>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public CreateBrokerCommandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Task<BrokerDto> Handle(CreateBrokerCommand request, CancellationToken cancellationToken)
    {
        return null;
    }

}
