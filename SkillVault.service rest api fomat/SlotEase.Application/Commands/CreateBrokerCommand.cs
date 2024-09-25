using MediatR;
using SlotEase.Application.DTO;

namespace SlotEase.Application.Commands
{
    // Sample command
    public class CreateBrokerCommand : IRequest<BrokerDto>
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public CreateBrokerCommand(BrokerDto brokerDto)
        {
            Id = brokerDto.Id;
            Name = brokerDto.Name;
        }
    }
}
