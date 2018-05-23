using System;
using System.Linq.Expressions;

namespace OFoods.Specifications
{
    /// <summary>
    /// 表示由相应的LINQ表达式表示的规范.
    /// </summary>
    /// <typeparam name="T">应用规范的对象的类型.</typeparam>
    internal sealed class ExpressionSpecification<T> : Specification<T>
    {
        #region 私有字段
        private Expression<Func<T, bool>> expression;
        #endregion

        #region 构造函数
        /// <summary>
        /// Initializes a new instance of <c>ExpressionSpecification&lt;T&gt;</c> class.
        /// </summary>
        /// <param name="expression">The LINQ expression which represents the current
        /// specification.</param>
        public ExpressionSpecification(Expression<Func<T, bool>> expression)
        {
            this.expression = expression;
        }
        #endregion

        #region 公开方法
        /// <summary>
        /// Gets the LINQ expression which represents the current specification.
        /// </summary>
        /// <returns>The LINQ expression.</returns>
        public override Expression<Func<T, bool>> GetExpression()
        {
            return this.expression;
        }
        #endregion
    }
}
