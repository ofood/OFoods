using System;
using System.Linq.Expressions;

namespace OFoods.Specifications
{
    /// <summary>
    /// 在任何情况下代表给定对象可以满足的规格.
    /// </summary>
    /// <typeparam name="T">应用规范的对象的类型.</typeparam>
    public sealed class NoneSpecification<T> : Specification<T>
    {
        #region 公用方法
        /// <summary>
        /// 获取表示当前规范的LINQ表达式.
        /// </summary>
        /// <returns>LINQ 表达式.</returns>
        public override Expression<Func<T, bool>> GetExpression()
        {
            return o => false;
        }
        #endregion
    }
}
