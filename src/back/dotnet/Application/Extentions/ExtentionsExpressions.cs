using System.Linq.Expressions;

namespace Application.Extensions;

internal static class ExtentionsExpressions
{
    internal static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expression, Expression<Func<T, bool>> andExpression)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var body = Expression.AndAlso(Expression.Invoke(expression, parameter), Expression.Invoke(andExpression, parameter));
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    internal static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expression, Expression<Func<T, bool>> orExpression)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var body = Expression.OrElse(Expression.Invoke(expression, parameter), Expression.Invoke(orExpression, parameter));
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }
}
