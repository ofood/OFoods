using System;
using System.Linq.Expressions;

namespace OFoods.Specifications
{
    /// <summary>
    /// 表示组合规范，它表明给定对象应满足给定规范.
    /// </summary>
    /// <typeparam name="T">应用规范的对象的类型.</typeparam>
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        #region 构造函数
        /// <summary>
        /// 构造<c> AndSpecification<T></ c>类的新实例.
        /// </summary>
        /// <param name="left">第一个规范.</param>
        /// <param name="right">第二个规范.</param>
        public AndSpecification(ISpecification<T> left, ISpecification<T> right) : base(left, right) { }
        #endregion

        #region 公用方法
        /// <summary>
        /// 获取表示当前规范的LINQ表达式.
        /// </summary>
        /// <returns>LINQ 表达式.</returns>
        public override Expression<Func<T, bool>> GetExpression()
        {
            //var body = Expression.AndAlso(Left.GetExpression().Body, Right.GetExpression().Body);
            //return Expression.Lambda<Func<T, bool>>(body, Left.GetExpression().Parameters);
            return Left.GetExpression().And(Right.GetExpression());
        }
        #endregion
    }

}
