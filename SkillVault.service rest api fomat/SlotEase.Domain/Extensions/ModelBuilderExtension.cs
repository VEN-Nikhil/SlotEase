using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SlotEase.Domain.Extensions
{
    public static class ModelBuilderExtension
    {
        /// <summary>
        /// Global filter to apply delted filter to all entity classes
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="modelBuilder"></param>
        /// <param name="expression"></param>
        public static void ApplyGlobalFilters<TInterface>(this ModelBuilder modelBuilder, Expression<Func<TInterface, bool>> expression)
        {
            IEnumerable<Type> entities = modelBuilder.Model
                .GetEntityTypes()
                .Where(e => e.ClrType.GetInterface(typeof(TInterface).Name) != null)
                .Select(e => e.ClrType);
            foreach (Type entity in entities)
            {
                ParameterExpression newParam = Expression.Parameter(entity);
                Expression newbody = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), newParam, expression.Body);
                modelBuilder.Entity(entity).HasQueryFilter(Expression.Lambda(newbody, newParam));
            }
        }
    }
}
