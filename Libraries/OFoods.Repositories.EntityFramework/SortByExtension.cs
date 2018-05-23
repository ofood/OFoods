using OFoods.Domain;
using OFoods.Specifications;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace OFoods.Repositories.EntityFramework
{
    /// <summary>
    /// 表示对实体框架存储库进行排序的方法扩展.
    /// </summary>
    internal static class SortByExtension
    {
        #region Internal Methods

        internal static IOrderedQueryable<TAggregateRoot> SortBy<TKey, TAggregateRoot>(
            this IQueryable<TAggregateRoot> query,
            Expression<Func<TAggregateRoot, dynamic>> sortPredicate) 
            where TAggregateRoot : class, IAggregateRoot<TKey>
            where TKey:IEquatable<TKey>
        {
            return InvokeSortBy<TKey, TAggregateRoot>(query, sortPredicate, SortOrder.Ascending);
        }

        internal static IOrderedQueryable<TAggregateRoot> SortByDescending<TKey, TAggregateRoot>(
            this IQueryable<TAggregateRoot> query,
            Expression<Func<TAggregateRoot, dynamic>> sortPredicate) 
            where TAggregateRoot : class, IAggregateRoot<TKey>
            where TKey:IEquatable<TKey>
        {
            return InvokeSortBy<TKey, TAggregateRoot>(query, sortPredicate, SortOrder.Descending);
        }

        #endregion

        #region Private Methods

        private static IOrderedQueryable<TAggregateRoot> InvokeSortBy<TKey, TAggregateRoot>(IQueryable<TAggregateRoot> query,Expression<Func<TAggregateRoot, dynamic>> sortPredicate,SortOrder sortOrder) 
            where TAggregateRoot : class, IAggregateRoot<TKey>
            where TKey:IEquatable<TKey>
        {
            var param = sortPredicate.Parameters[0];
            string propertyName = null;
            Type propertyType = null;
            Expression bodyExpression = null;
            if (sortPredicate.Body is UnaryExpression)
            {
                UnaryExpression unaryExpression = sortPredicate.Body as UnaryExpression;
                bodyExpression = unaryExpression.Operand;
            }
            else if (sortPredicate.Body is MemberExpression)
            {
                bodyExpression = sortPredicate.Body;
            }
            else throw new ArgumentException(@"排序谓词表达式的主体应该是UnaryExpression或MemberExpression.", "sortPredicate");
            MemberExpression memberExpression = (MemberExpression)bodyExpression;
            propertyName = memberExpression.Member.Name;
            if (memberExpression.Member.MemberType == MemberTypes.Property)
            {
                PropertyInfo propertyInfo = memberExpression.Member as PropertyInfo;
                propertyType = propertyInfo.PropertyType;
            }
            else throw new InvalidOperationException(@"由于排序谓词表达式表示的成员表达式不包含PropertyInfo对象，因此无法评估属性的类型.");

            Type funcType = typeof(Func<,>).MakeGenericType(typeof(TAggregateRoot), propertyType);
            LambdaExpression convertedExpression = Expression.Lambda(
                funcType,
                Expression.Convert(Expression.Property(param, propertyName), propertyType),
                param);

            var sortingMethods = typeof(Queryable).GetMethods(BindingFlags.Public | BindingFlags.Static);
            var sortingMethodName = GetSortingMethodName(sortOrder);
            var sortingMethod =
                sortingMethods.Where(
                    sm => sm.Name == sortingMethodName && sm.GetParameters() != null && sm.GetParameters().Length == 2)
                    .First();
            return
                (IOrderedQueryable<TAggregateRoot>)
                sortingMethod.MakeGenericMethod(typeof(TAggregateRoot), propertyType)
                    .Invoke(null, new object[] { query, convertedExpression });
        }

        private static string GetSortingMethodName(SortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    return "OrderBy";
                case SortOrder.Descending:
                    return "OrderByDescending";
                default:
                    throw new ArgumentException(
                        "排序顺序必须指定为Ascending或Descending.",
                        "sortOrder");
            }
        }

        #endregion
    }
}
