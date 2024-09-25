using System.Collections.Generic;

namespace SlotEase.Application.Interfaces.Setting;

public interface IAppConfigurationQueries
{
    List<KeyValuePair<string, string>> GetConfigurations();
}
