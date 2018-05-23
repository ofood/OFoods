namespace OFoods.Generators
{
    /// <summary>
    /// 表示实现的类是标识生成器.
    /// </summary>
    public interface IIdentityGenerator
    {
        /// <summary>
        /// 生成标识.
        /// </summary>
        /// <returns>生成的标识实例.</returns>
        object Generate();
    }
}
