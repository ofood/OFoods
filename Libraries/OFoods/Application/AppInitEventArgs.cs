using System;
using OFoods.Configurations;
using OFoods.Dependency;
namespace OFoods.Application
{
    public class AppInitEventArgs:EventArgs
    {
        /// <summary>
        /// 获取用于配置应用程序的<see cref ="IConfigSource"/>实例.
        /// </summary>
        public IConfigSource ConfigSource { get; private set; }
        /// <summary>
        /// 获取应用程序注册或解析对象依赖关系的<see cref ="IObjectContainer"/>实例.
        /// </summary>
        public ObjectContainer ObjectContainer { get; private set; }
        /// <summary>
        /// 初始化<c> AppInitEventArgs </ c>类的新实例.
        /// </summary>
        public AppInitEventArgs():this(null,null)
        {

        }
        /// <summary>
        /// 初始化<c> AppInitEventArgs </ c>类的新实例.
        /// </summary>
        /// <param name="configSource">用于配置应用程序的<see cref ="IConfigSource"/>实例.</param>
        /// <param name="objectContainer">应用程序注册或解析对象依赖关系的<see cref ="IObjectContainer"/>实例.</param>
        public AppInitEventArgs(IConfigSource configSource, ObjectContainer objectContainer)
        {
            ConfigSource = configSource;
            ObjectContainer = objectContainer;
        }
    }
}
