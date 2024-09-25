using System.ComponentModel.DataAnnotations;

namespace SlotEase.Application.DTO;

/// sample dto
public class BrokerDto
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; }
}
