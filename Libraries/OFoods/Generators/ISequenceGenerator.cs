namespace OFoods.Generators
{
    /// <summary>
    /// 表示实现的类是序列生成器.
    /// </summary>
    public interface ISequenceGenerator
    {
        /// <summary>
        /// 获取序列的下一个值.
        /// </summary>
        object Next { get; }
    }
}
