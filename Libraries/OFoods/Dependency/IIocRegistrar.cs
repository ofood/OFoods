
using System;
using System.Reflection;

namespace OFoods.Dependency
{
    /// <summary>
    /// 定义用于注册依赖关系的类的接口.
    /// </summary>
    public interface IIocRegistrar
    {
        /// <summary>
        /// 将类型注册为自注册.
        /// </summary>
        /// <param name="type">类的类型</param>
        /// <param name="serviceName">服务名称.</param>
        /// <param name="lifeStyle">这个类型对象的Lifestyle</param>
        void Register(Type type, string serviceName = null, LifetimeStyle lifeStyle = LifetimeStyle.Singleton);
        /// <summary>
        /// 将类型注册为自注册.
        /// </summary>
        /// <typeparam name="T">类的类型</typeparam>
        /// <param name="serviceName">服务名称.</param>
        /// <param name="lifeStyle">这个对象类型的Lifestyle</param>
        void Register<T>(string serviceName = null, LifetimeStyle lifeStyle = LifetimeStyle.Singleton)
            where T : class;
        /// <summary>
        /// 注册一个实现它的类型.
        /// </summary>
        /// <typeparam name="TType">注册类型</typeparam>
        /// <typeparam name="TImpl">实现<see cref ="TType"/>的类型</typeparam>
        /// <param name="serviceName">服务名称.</param>
        /// <param name="lifeStyle">Lifestyle 这种类型的对象</param>
        void Register<TType, TImpl>(string serviceName = null, LifetimeStyle lifeStyle = LifetimeStyle.Singleton)
            where TType : class
            where TImpl : class, TType;

        /// <summary>
        /// 注册一个实现它的类型.
        /// </summary>
        /// <param name="type">类的类型</param>
        /// <param name="impl">实现<paramref name =“type”/>的类型</param>
        /// <param name="serviceName">服务名称.</param>
        /// <param name="lifeStyle">这个类的LifeStyle</param>
        void Register(Type type, Type impl, string serviceName = null, LifetimeStyle lifeStyle = LifetimeStyle.Singleton);

        /// <summary>
        /// 将实现类型实例注册为服务实现.
        /// </summary>
        /// <typeparam name="TService">服务类型.</typeparam>
        /// <typeparam name="TImplementer">实现类型.</typeparam>
        /// <param name="instance">实现类型实例.</param>
        /// <param name="serviceName">服务名称.</param>
        void RegisterInstance<TService, TImplementer>(TImplementer instance, string serviceName = null)
            where TService : class
            where TImplementer : class, TService;
        
        /// <summary>
        /// 检查给定类型是否在之前注册.
        /// </summary>
        /// <param name="type">类型检查</param>
        bool IsRegistered(Type type);

        /// <summary>
        /// 检查给定类型是否在之前注册.
        /// </summary>
        /// <typeparam name="TType">类型检查</typeparam>
        bool IsRegistered<TType>();
    }
}
