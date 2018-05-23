using System;
using System.Linq.Expressions;

namespace OFoods.Specifications
{
    /// <summary>
    /// 表示指定与给定规范相反的语义的规范.
    /// </summary>
    /// <typeparam name="T">应用规范的对象的类型.</typeparam>
    public class NotSpecification<T> : Specification<T>
    {
        #region 私有字段
        private ISpecification<T> spec;
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化<c> NotSpecification <T> </ c>类的新实例.
        /// </summary>
        /// <param name="specification">规格被反转.</param>
        public NotSpecification(ISpecification<T> specification)
        {
            this.spec = specification;
        }
        #endregion

        #region 公用方法
        /// <summary>
        /// 获取表示当前规范的LINQ表达式.
        /// </summary>
        /// <returns>LINQ 表达式.</returns>
        public override Expression<Func<T, bool>> GetExpression()
        {
            var body = Expression.Not(this.spec.GetExpression().Body);
            return Expression.Lambda<Func<T, bool>>(body, this.spec.GetExpression().Parameters);
        }
        #endregion
    }
}
