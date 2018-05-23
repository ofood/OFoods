using System;
using System.Linq.Expressions;

namespace OFoods.Specifications
{
    /// <summary>
    /// 表示组合的规格，表明给定规格中的任一规格应由给定的对象满足.
    /// </summary>
    /// <typeparam name="T">应用规范的对象的类型.</typeparam>
    public class OrSpecification<T> : CompositeSpecification<T>
    {
        #region 构造函数
        /// <summary>
        /// 初始化<c> OrSpecification <T> </ c>类的新实例.
        /// </summary>
        /// <param name="left">第一个规范.</param>
        /// <param name="right">第二个规范.</param>
        public OrSpecification(ISpecification<T> left, ISpecification<T> right) : base(left, right) { }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取表示当前规范的LINQ表达式.
        /// </summary>
        /// <returns>LINQ 表达式.</returns>
        public override Expression<Func<T, bool>> GetExpression()
        {
            //var body = Expression.OrElse(Left.GetExpression().Body, Right.GetExpression().Body);
            //return Expression.Lambda<Func<T, bool>>(body, Left.GetExpression().Parameters);
            return Left.GetExpression().Or(Right.GetExpression());
        }
        #endregion
    }
}
