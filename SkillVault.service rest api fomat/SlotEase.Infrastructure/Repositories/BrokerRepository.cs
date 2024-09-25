using System;
using System.Collections.Generic;
using Yodaquoter.AgentPortal.Domain.Entities;
using Yodaquoter.AgentPortal.Infrastructure;

namespace Yodaquoter.VideoChat.API.Infrastructure.Repositories
{
    public class BrokerRepository : BaseRepository<Broker>
    {
        private readonly AgentPortalContext _agentPortalContext;

        public BrokerRepository(AgentPortalContext videoChatContext) :
            base(videoChatContext)
        {
            _agentPortalContext = videoChatContext ?? throw new ArgumentNullException(nameof(videoChatContext));
        }

        public IEnumerable<Broker> GetBrokerUsers()
        {
            return Get();
        }

        public void AddBroker(Broker agent)
        {
            Insert(agent);
        }

        public void UpdateBroker(Broker agent)
        {
            Update(agent);
        }

        public void Save()
        {
            _agentPortalContext.SaveChanges();
        }
    }
}
