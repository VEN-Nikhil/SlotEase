using System.Collections.Generic;
using System.Linq;
using SlotEase.Application.Interfaces.Setting;
using SlotEase.Domain.Entities.Setting;
using SlotEase.Domain.Interfaces;

namespace SlotEase.Application.Queries.Setting
{
    public class AppConfigurationQueries : IAppConfigurationQueries
    {
        private readonly IRepository<AppConfigurationUI> _configrationRepository;

        public AppConfigurationQueries(IRepository<AppConfigurationUI> configrationRepository)
        {
            _configrationRepository = configrationRepository;
        }

        public List<KeyValuePair<string, string>> GetConfigurations()
        {
            List<KeyValuePair<string, string>> configurations = _configrationRepository.GetAll().Select(x => new KeyValuePair<string, string>(x.Name, x.Value)).ToList();
            return configurations;
        }
    }
}
