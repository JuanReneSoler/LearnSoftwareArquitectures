using System.Linq.Expressions;

namespace Application.Share.Extensions;

public static class StringToLamdaExpression
{
    public static Expression<Func<TSource, TKey>> ToLamdaExpression<TSource, TKey>(this string ObjectNavigation)
    {
        if(string.IsNullOrEmpty(ObjectNavigation) || string.IsNullOrWhiteSpace(ObjectNavigation))
            throw new NullReferenceException("Incorrect Expression.");

        var param = Expression.Parameter(typeof(TSource), "prop");
        Expression body = param;

        foreach (var propertyStr in ObjectNavigation.Split('.'))
        {
            body = Expression.Property(body, propertyStr);
        }

        var lamda = Expression.Lambda(body, param);

        return (Expression<Func<TSource, TKey>>)lamda;
    }
}
