using System.Collections.Generic;
using System.Linq.Expressions;

namespace OFoods.Specifications
{
    /// <summary>
    /// 表示用于重新绑定给定表达式的参数的参数rebinder。 
    /// 这是解决方案的一部分，
    /// 该解决方案通过使用Apworks规范来解决实体框架中的表达式参数问题。 
    /// 有关此解决方案的更多信息，请参阅
    /// http://blogs.msdn.com/b/meek/archive/2008/05/02/linq-to-entities-combining-predicates.aspx.
    /// </summary>
    internal class ParameterRebinder : ExpressionVisitor
    {
        #region 私有字段
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;
        #endregion

        #region 构造函数
        internal ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }
        #endregion

        #region 内部静态方法
        internal static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }
        #endregion

        #region 受保护的方法
        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
        #endregion
    }
}
