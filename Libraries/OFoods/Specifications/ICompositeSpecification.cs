namespace OFoods.Specifications
{
    /// <summary>
    /// 表示实现的类是复合规范.
    /// </summary>
    /// <typeparam name="T">应用规范的对象的类型.</typeparam>
    public interface ICompositeSpecification<T> : ISpecification<T>
    {
        /// <summary>
        /// 获取规范的左侧.
        /// </summary>
        ISpecification<T> Left { get; }
        /// <summary>
        /// 获取规范的右侧.
        /// </summary>
        ISpecification<T> Right { get; }
    }
}
