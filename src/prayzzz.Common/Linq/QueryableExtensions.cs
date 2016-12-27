using System;
using System.Linq;
using System.Linq.Expressions;
using prayzzz.Common.Enums;

namespace prayzzz.Common.Linq
{
    public static class QueryableExtensions
    {
        public static IOrderedQueryable<T> OrderBy<T, TOrder>(this IQueryable<T> list, Expression<Func<T, TOrder>> order, OrderingDirection direction = OrderingDirection.Asc)
        {
            if (direction == OrderingDirection.Desc)
            {
                return list.OrderByDescending(order);
            }

            return Queryable.OrderBy(list, order);
        }
    }
}