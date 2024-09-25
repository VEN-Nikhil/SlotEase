using System.Collections.Generic;

namespace SlotEase.Application.DTO.Audited;

public class ListResultDto<T>
{
    public ListResultDto(IReadOnlyList<T> items)
    {
        Items = items;
    }
    public IReadOnlyList<T> Items { get; set; }
}
