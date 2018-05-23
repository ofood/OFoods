using System;
using System.Linq;
using System.Linq.Expressions;

namespace OFoods.Specifications
{
    /// <summary>
    /// 表示Expression [Func [T，bool]]类型的扩展器.
    /// 这是解决方案的一部分
    /// 使用Apworks规范转到实体框架时的表达式参数问题
    /// 有关此解决方案的更多信息，请参阅
    ///  http://blogs.msdn.com/b/meek/archive/2008/05/02/linq-to-entities-combining-predicates.aspx.
    /// </summary>
    public static class ExpressionFuncExtender
    {
        #region 私有方法
        private static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression 
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }
        #endregion

        #region 公用方法
        /// <summary>
        /// 通过使用AND语义组合两个给定表达式.
        /// </summary>
        /// <typeparam name="T">对象的类型.</typeparam>
        /// <param name="first">表达式的第一部分.</param>
        /// <param name="second">表达式的第二部分.</param>
        /// <returns>组合的表达.</returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }
        /// <summary>
        /// 通过使用OR语义组合两个给定表达式.
        /// </summary>
        /// <typeparam name="T">对象的类型.</typeparam>
        /// <param name="first">表达式的第一部分.</param>
        /// <param name="second">表达式的第二部分.</param>
        /// <returns>组合的表达.</returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.OrElse);
        }
        #endregion
    }
}
