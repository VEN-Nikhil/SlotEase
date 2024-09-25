using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlotEase.Domain.Entities.Setting;

[Table("AppConfigurations")]
public class AppConfigurationUI : Entity<int>
{
    [MaxLength(50), Required]
    public string Name { get; set; }
    public string Value { get; set; }
}
