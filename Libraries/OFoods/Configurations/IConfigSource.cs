namespace OFoods.Configurations
{
    /// <summary>
    /// 表示实现的类是OFood框架的配置源.
    /// </summary>
    public interface IConfigSource
    {
        /// <summary>
        /// 获取<see cref ="OFoodsConfigSection"/>类的实例.
        /// </summary>
        OFoodsConfigSection Config { get; }
    }
}
