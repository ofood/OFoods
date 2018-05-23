using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OFoods.Domain
{
    public static class Utils
    {
        /// <summary>
        /// 构建将被任何LINQ提供者用来检查两个聚合根是否具有相同标识符的标识符等于谓词.
        /// </summary>
        /// <typeparam name="TKey">key的类型.</typeparam>
        /// <typeparam name="TAggregateRoot">聚合根类型.</typeparam>
        /// <param name="id">要检查的标识符值.</param>
        /// <returns>生成的Lambda表达式.</returns>
        public static Expression<Func<TAggregateRoot, bool>> BuildIdEqualsPredicate<TKey, TAggregateRoot>(TKey id)
            where TAggregateRoot : IAggregateRoot<TKey>
            where TKey : IEquatable<TKey>
        {
            var parameter = Expression.Parameter(typeof(TAggregateRoot));
            return
                Expression.Lambda<Func<TAggregateRoot, bool>>(
                    Expression.Equal(Expression.Property(parameter, "ID"), Expression.Constant(id)),
                    parameter);
        }
    }
}
