using System;
using System.Linq;
using System.Linq.Expressions;

namespace SlotEase.Domain.Extensions;

public static class WhereIfExtension
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
    {
        return condition
            ? query.Where(predicate)
            : query;
    }
}
