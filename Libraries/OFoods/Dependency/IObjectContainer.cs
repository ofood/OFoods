using System;

namespace OFoods.Dependency
{
    /// <summary>
    /// 表示一个对象容器接口.
    /// </summary>
    public interface IObjectContainer:IIocRegistrar,IIocResolver,IDisposable
    {
        /// <summary>
        /// 构建容器.
        /// </summary>
        void Build();
        /// <summary>
        /// 使用配置文件初始化对象容器.
        /// </summary>
        /// <param name="configPath">用于初始化对象容器的配置文件路径.</param>
        void InitializeFromConfigFile(string configPath);
    }
}
