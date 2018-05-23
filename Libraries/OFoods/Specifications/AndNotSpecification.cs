using System;
using System.Linq.Expressions;

namespace OFoods.Specifications
{
    /// <summary>
    /// 表示组合规范，它表示第一个规范可以由给定对象满足，而第二个规范不能满足.
    /// </summary>
    /// <typeparam name="T">应用规范的对象的类型.</typeparam>
    public class AndNotSpecification<T> : CompositeSpecification<T>
    {
        #region Ctor
        /// <summary>
        /// 构造<c> AndNotSpecification<T></ c>类的新实例
        /// </summary>
        /// <param name="left">第一个规范.</param>
        /// <param name="right">第二个规范.</param>
        public AndNotSpecification(ISpecification<T> left, ISpecification<T> right) : base(left, right) { }
        #endregion

        #region 公用方法
        /// <summary>
        /// 获取表示当前规范的LINQ表达式.
        /// </summary>
        /// <returns>LINQ 表达式.</returns>
        public override Expression<Func<T, bool>> GetExpression()
        {
            var bodyNot = Expression.Not(Right.GetExpression().Body);
            var bodyNotExpression = Expression.Lambda<Func<T, bool>>(bodyNot, Right.GetExpression().Parameters);

            return Left.GetExpression().And(bodyNotExpression);
        }
        #endregion
    }
}
