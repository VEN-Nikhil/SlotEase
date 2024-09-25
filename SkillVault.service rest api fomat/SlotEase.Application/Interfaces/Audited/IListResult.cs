using System.Collections.Generic;

namespace SlotEase.Application.Interfaces.Audited
{
    public interface IListResult<T>
    {
        IReadOnlyList<T> Items { get; set; }
    }
}
