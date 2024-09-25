using System.Collections.Generic;

namespace SlotEase.Application.DTO.Page;

public class PagedResultDto<T>
{
    public IReadOnlyList<T> Items { get; set; }
    public int TotalCount { get; set; }

    public PagedResultDto(int totalCount, IReadOnlyList<T> items)
    {
        TotalCount = totalCount;
        Items = items;
    }
}
