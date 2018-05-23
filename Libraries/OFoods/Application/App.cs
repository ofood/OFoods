using Castle.DynamicProxy;
using OFoods.Configurations;
using OFoods.Configurations.Elements;
using System;
using System.Collections.Generic;
using OFoods.Dependency;
namespace OFoods.Application
{
    /// <summary>
    /// 代表应用程序的实现.
    /// </summary>
    public class App:IApp
    {
        private readonly IConfigSource configSource;
        private readonly ObjectContainer objectContainer;
        private readonly List<IInterceptor> interceptors;
        /// <summary>
        /// 初始化<c> Application </ c>类的新实例.
        /// </summary>
        /// <param name="configSource">
        /// <see cref ="IConfigSource"/>用于配置应用程序的实例.
        /// </param>
        public App(IConfigSource configSource)
        {
            if (configSource == null)
                throw new ArgumentNullException(nameof(configSource));
            if (configSource.Config == null)
                throw new ConfigException("OFoodConfigSection尚未在ConfigSource实例中定义.");
            if (configSource.Config.ObjectContainer == null)
                throw new ConfigException("在OFoodConfigSection中没有指定ObjectContainer实例.");
            this.configSource = configSource;
            string objectContainerProviderName = configSource.Config.ObjectContainer.Provider;
            if (string.IsNullOrEmpty(objectContainerProviderName) ||
                string.IsNullOrWhiteSpace(objectContainerProviderName))
                throw new ConfigException("ObjectContainer提供程序尚未在ConfigSource中定义.");
            Type objectContainerType = Type.GetType(objectContainerProviderName);
            if (objectContainerType == null)
                throw new OFoodsException(string.Format("由类型{0}定义的ObjectContainer不存在.", objectContainerProviderName));
            objectContainer = (ObjectContainer)Activator.CreateInstance(objectContainerType);
            if (configSource.Config.ObjectContainer.InitFromConfigFile)
            {
                string sectionName = this.configSource.Config.ObjectContainer.SectionName;
                if (!string.IsNullOrEmpty(sectionName) && !string.IsNullOrWhiteSpace(sectionName))
                {
                    objectContainer.InitializeFromConfigFile(sectionName);
                    
                }
                else
                    throw new ConfigException("当InitFromConfigFile已设置为true时，还应提供ObjectContainer配置的节名称.");
            }
            interceptors = new List<IInterceptor>();
            if (this.configSource.Config.Interception != null &&
                this.configSource.Config.Interception.Interceptors != null)
            {
                foreach (InterceptorElement interceptorElement in this.configSource.Config.Interception.Interceptors)
                {
                    Type interceptorType = Type.GetType(interceptorElement.Type);
                    if (interceptorType == null)
                        continue;
                    IInterceptor interceptor = (IInterceptor)Activator.CreateInstance(interceptorType);
                    interceptors.Add(interceptor);
                }
            }
        }

        #region 私有方法
        private void DoInitialize()
        {
            Initialize?.Invoke(this, new AppInitEventArgs(configSource, objectContainer));
        }
        #endregion

        #region 受保护方法
        /// <summary>
        /// 启动应用程序
        /// </summary>
        protected virtual void OnStart() { }
        #endregion

        #region IApplication 成员
        /// <summary>
        /// 获取用于配置应用程序的<see cref ="IConfigSource"/>实例.
        /// </summary>
        public IConfigSource ConfigSource
        {
            get { return configSource; }
        }
        /// <summary>
        /// 获取应用程序注册或解析对象依赖关系的<see cref ="IObjectContainer"/>实例.
        /// </summary>
        public ObjectContainer ObjectContainer
        {
            get { return objectContainer; }
        }
        /// <summary>
        /// 获取在当前应用程序中注册的<see cref ="IInterceptor"/>实例的列表.
        /// </summary>
        public IEnumerable<IInterceptor> Interceptors
        {
            get { return interceptors; }
        }
        /// <summary>
        /// 启动应用程序.
        /// </summary>
        public void Start()
        {
            DoInitialize();
            OnStart();
        }
        /// <summary>
        /// 应用程序初始化时发生的事件.
        /// </summary>
        public event EventHandler<AppInitEventArgs> Initialize;

        #endregion
    }
}
