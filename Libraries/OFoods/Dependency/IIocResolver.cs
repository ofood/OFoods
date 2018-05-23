using System;
namespace OFoods.Dependency
{
    /// <summary>
    /// 定义用于解决依赖关系的类的接口.
    /// </summary>
    public interface IIocResolver
    {
        /// <summary>
        /// 从IOC容器获取对象.
        /// 返回对象必须在使用后释放（请参阅<请参见cref =“Release”/>）.
        /// </summary> 
        /// <typeparam name="T">要获取的对象的类型</typeparam>
        /// <returns>对象实例</returns>
        T Resolve<T>();

        /// <summary>
        /// 从IOC容器获取对象.
        /// 返回对象必须在使用后释放（请参阅<请参见cref ="Release"/>）.
        /// </summary> 
        /// <typeparam name="T">要投射的物体的类型</typeparam>
        /// <param name="type">要解析的对象的类型</param>
        /// <returns>对象实例</returns>
        T Resolve<T>(Type type);

        /// <summary>
        /// 从IOC容器获取对象.
        /// 返回对象必须在使用后释放（请参阅<请参见cref =“Release”/>）.
        /// </summary> 
        /// <typeparam name="T">要获取的对象的类型</typeparam>
        /// <param name="argumentsAsAnonymousType">构造函数参数</param>
        /// <returns>对象实例</returns>
        T Resolve<T>(params object[] argumentsAsAnonymousType);

        /// <summary>
        /// 从IOC容器获取对象.
        /// 返回对象必须在使用后释放（请参阅<请参见cref =“Release”/>）.
        /// </summary> 
        /// <param name="type">要获取的对象的类型</param>
        /// <returns>对象实例</returns>
        object Resolve(Type type);

        /// <summary>
        /// 从IOC容器获取对象.
        /// 返回对象必须在使用后释放（请参阅<请参见cref =“Release”/>）.
        /// </summary> 
        /// <param name="type">要获取的对象的类型</param>
        /// <param name="argumentsAsAnonymousType">构造函数参数</param>
        /// <returns>对象实例</returns>
        object Resolve(Type type, params object[] argumentsAsAnonymousType);

        /// <summary>
        /// 获取给定类型的所有实现.
        /// 使用后必须释放返回的对象（请参阅<请参阅cref =“Release”/>）.
        /// </summary> 
        /// <typeparam name="T">要解析的对象的类型</typeparam>
        /// <returns>对象实例</returns>
        T[] ResolveAll<T>();

        /// <summary>
        /// 获取给定类型的所有实现.
        /// 使用后必须释放返回的对象（请参阅<请参阅cref =“Release”/>）.
        /// </summary> 
        /// <typeparam name="T">要解析的对象的类型</typeparam>
        /// <param name="argumentsAsAnonymousType">构造函数参数</param>
        /// <returns>对象实例</returns>
        T[] ResolveAll<T>(params object[] argumentsAsAnonymousType);

        /// <summary>
        /// 获取给定类型的所有实现.
        /// 使用后必须释放返回的对象（请参阅<请参阅cref =“Release”/>）.
        /// </summary> 
        /// <param name="type">要解析的对象的类型</param>
        /// <returns>对象实例</returns>
        object[] ResolveAll(Type type);

        /// <summary>
        /// 获取给定类型的所有实现.
        /// 使用后必须释放返回的对象（请参阅<请参阅cref =“Release”/>）.
        /// </summary> 
        /// <param name="type">要解析的对象的类型</param>
        /// <param name="argumentsAsAnonymousType">构造函数参数</param>
        /// <returns>对象实例</returns>
        object[] ResolveAll(Type type, params object[] argumentsAsAnonymousType);
        /// <summary>
        /// 尝试从容器中检索服务.
        /// </summary>
        /// <typeparam name="TService">要解析的服务类型.</typeparam>
        /// <param name="instance">生成的组件实例提供服务，或默认（TService）.</param>
        /// <returns>如果提供服务的组件可用，则为true.</returns>
        bool TryResolve<TService>(out TService instance) where TService : class;
        /// <summary>
        /// 尝试从容器中检索服务.
        /// </summary>
        /// <param name="serviceType">要解析的服务类型.</param>
        /// <param name="instance">生成的组件实例提供服务，或者为null.</param>
        /// <returns>如果提供服务的组件可用，则为true.</returns>
        bool TryResolve(Type serviceType, out object instance);
        /// <summary>
        /// 解析一项服务.
        /// </summary>
        /// <typeparam name="TService">服务类型.</typeparam>
        /// <param name="serviceName">服务名称.</param>
        /// <returns>提供服务的组件实例.</returns>
        TService ResolveNamed<TService>(string serviceName) where TService : class;
        /// <summary>
        /// 解析一项服务.
        /// </summary>
        /// <param name="serviceName">服务名.</param>
        /// <param name="serviceType">服务类型.</param>
        /// <returns>提供服务的组件实例.</returns>
        object ResolveNamed(string serviceName, Type serviceType);
        /// <summary>
        /// 尝试从容器中检索服务.
        /// </summary>
        /// <param name="serviceName">要解析的服务的名称.</param>
        /// <param name="serviceType">要解析的服务的类型.</param>
        /// <param name="instance">生成的组件实例提供服务，或者为null.</param>
        /// <returns>如果提供服务的组件可用，则为true.</returns>
        bool TryResolveNamed(string serviceName, Type serviceType, out object instance);

       
        
    }
}
