using OFoods.Configurations;
using System;
namespace OFoods.Application
{
    /// <summary>
    /// 代表应用程序创建，初始化和启动的应用程序运行时. 
    /// </summary>
    public sealed class AppRuntime
    {
        private static readonly AppRuntime instance = new AppRuntime();
        private static readonly object lockObj = new object();
        private IApp currentApplication = null;
        static AppRuntime()
        {
        }

        private AppRuntime()
        {
        }
        /// <summary>
        /// 获取当前<c> ApplicationRuntime </ c>类的实例. 
        /// </summary>
        public static AppRuntime Instance
        {
            get { return instance; }
        }
        /// <summary>
        /// 获取当前正在运行的应用程序的实例. 
        /// </summary>
        public IApp CurrentApplication
        {
            get { return currentApplication; }
        }
        /// <summary>
        /// 创建并初始化新的应用程序实例. 
        /// </summary>
        /// <param name="configSource">
        /// <see cref ="IConfigSource"/>用于初始化应用程序的实例.
        /// </param>
        /// <returns> 初始化的应用程序实例. </returns>
        public static IApp Create(IConfigSource configSource)
        {
            lock (lockObj)
            {
                if (instance.currentApplication == null)
                {
                    lock (lockObj)
                    {
                        if (configSource.Config == null ||
                            configSource.Config.Application == null)
                            throw new ConfigException("OFood配置或OFood应用程序配置尚未在ConfigSource实例中初始化.");
                        string typeName = configSource.Config.Application.Provider;
                        if (string.IsNullOrEmpty(typeName))
                            throw new ConfigException("提供程序类型尚未在ConfigSource中定义.");
                        Type type = Type.GetType(typeName);
                        if (type == null)
                            throw new OFoodsException(string.Format("由类型'{0}'定义的应用程序提供者不存在.", typeName));
                        instance.currentApplication = (IApp)Activator.CreateInstance(type, new object[] { configSource });
                    }
                }
            }
            return instance.currentApplication;
        }
    }
}
