using System;
using System.Collections.Generic;
using Yodaquoter.AgentPortal.Domain.Entities;
using Yodaquoter.AgentPortal.Infrastructure;

namespace Yodaquoter.VideoChat.API.Infrastructure.Repositories
{
    public class AgentRepository : BaseRepository<Agent>
    {
        private readonly AgentPortalContext _agentPortalContext;

        public AgentRepository(AgentPortalContext videoChatContext) :
            base(videoChatContext)
        {
            _agentPortalContext = videoChatContext ?? throw new ArgumentNullException(nameof(videoChatContext));
        }

        public IEnumerable<Agent> GetAgent()
        {
            return Get();
        }

        public void AddAgent(Agent agent)
        {
            Insert(agent);
        }

        public void UpdateAgent(Agent agent)
        {
            Update(agent);
        }



        public void Save()
        {
            _agentPortalContext.SaveChanges();
        }
    }
}
