using Castle.DynamicProxy;
using OFoods.Configurations;
using OFoods.Dependency;
using System;
using System.Collections.Generic;

namespace OFoods.Application
{
    /// <summary>
    /// 表示实现的类是OFood应用程序
    /// </summary>
    public interface IApp
    {
        /// <summary>
        /// 获取用于配置应用程序的<see cref ="IConfigSource"/>实例.
        /// </summary>
        IConfigSource ConfigSource { get; }
        /// <summary>
        /// 获取应用程序注册或解析对象依赖关系的<see cref ="OFoods.IObjectContainer"/>实例.
        /// </summary>
        ObjectContainer ObjectContainer { get; }

        /// <summary>
        /// 获取在当前应用程序中注册的<see cref ="IInterceptor"/>实例的列表.
        /// </summary>
        IEnumerable<IInterceptor> Interceptors { get; }

        /// <summary>
        /// 启动应用程序. 
        /// </summary>
        void Start();
        /// <summary>
        /// 应用程序初始化时发生的事件. 
        /// </summary>
        event EventHandler<AppInitEventArgs> Initialize;
    }
}
