namespace OFoods.Specifications
{
    /// <summary>
    /// 表示实现的类是规范解析器，它将给定的规范解析为特定于域的条件对象，
    /// 例如NHibernate中的<c> ICriteria </ c>实例.
    /// </summary>
    /// <typeparam name="TCriteria">域特定标准的类型.</typeparam>
    public interface ISpecificationParser<TCriteria>
    {
        /// <summary>
        /// 将给定的规范解析为特定于域的条件对象.
        /// </summary>
        /// <typeparam name="T">应用规范的对象的类型.</typeparam>
        /// <param name="specification">指定的规范实例.</param>
        /// <returns>特定于域的标准的实例.</returns>
        TCriteria Parse<T>(ISpecification<T> specification);
    }
}
