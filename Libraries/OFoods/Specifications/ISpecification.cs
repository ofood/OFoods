using System;
using System.Linq.Expressions;

namespace OFoods.Specifications
{
    /// <summary>
    /// 表示实现的类是规范。 有关规格模式的更多信息，请参阅
    /// http://martinfowler.com/apsupp/spec.pdf.
    /// </summary>
    /// <typeparam name="T">应用规范的对象的类型.</typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        /// 返回一个<see cref ="System.Boolean"/>值，该值指示给定对象是否满足指定范围.
        /// </summary>
        /// <param name="obj">规范应用的对象.</param>
        /// <returns>如果规格满足，则为真，否则为false.</returns>
        bool IsSatisfiedBy(T obj);
        /// <summary>
        /// 将当前规范实例与另一个规范实例组合在一起，并返回组合规范，
        /// 该规范表示当前和给定规范必须由给定对象满足.
        /// </summary>
        /// <param name="other">与当前规范组合的规范实例.</param>
        /// <returns>组合的规范实例.</returns>
        ISpecification<T> And(ISpecification<T> other);
        /// <summary>
        /// 将当前规范实例与另一个规范实例组合，
        /// 并返回组合规范，该规范表示给定对象应满足当前规范或给定规范.
        /// </summary>
        /// <param name="other">与当前规范组合的规范实例.</param>
        /// <returns>组合的规范实例.</returns>
        ISpecification<T> Or(ISpecification<T> other);
        /// <summary>
        /// 将当前规范实例与另一个规范实例组合在一起，并返回组合规范，
        /// 该规范表示当前规范应该由给定对象满足，但规定的规范不应该
        /// </summary>
        /// <param name="other">与当前规范组合的规范实例.</param>
        /// <returns>组合的规范实例.</returns>
        ISpecification<T> AndNot(ISpecification<T> other);
        /// <summary>
        /// 颠倒当前规范实例并返回一个规范，该规范表示与当前规范相反的语义.
        /// </summary>
        /// <returns>颠倒过来的规范实例.</returns>
        ISpecification<T> Not();
        /// <summary>
        /// 获取表示当前规范的LINQ表达式.
        /// </summary>
        /// <returns>LINQ 表达式.</returns>
        Expression<Func<T, bool>> GetExpression();
    }
}
