
using System.Linq.Expressions;

namespace Infrastructure.Extentions;

public static class ExtentionsExpressions
{
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expression, Expression<Func<T, bool>> andExpression)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var body = Expression.AndAlso(Expression.Invoke(expression, parameter), Expression.Invoke(andExpression, parameter));
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }
}
