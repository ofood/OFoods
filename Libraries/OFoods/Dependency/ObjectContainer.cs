using Castle.DynamicProxy;
using OFoods.Application;
using OFoods.Interception;
using System;
using System.Collections.Generic;
using System.Linq;


namespace OFoods.Dependency
{
    /// <summary>
    /// 表示对象容器.
    /// </summary>
    public abstract class ObjectContainer:IObjectContainer
    {
        private  readonly IInterceptorSelector interceptorSelector = new InterceptorSelector();
        private  readonly ProxyGenerator proxyGenerator = new ProxyGenerator();
        private  readonly ProxyGenerationOptions proxyGenerationOptions;
        public ObjectContainer()
        {
            proxyGenerationOptions = new ProxyGenerationOptions { Selector = interceptorSelector };
        }
        /// <summary>
        /// 使用application/web config文件初始化对象容器.
        /// </summary>
        /// <param name="configSectionName">用于初始化对象容器的application/web配置文件中的ConfigurationSection的名称.</param>
        public abstract void InitializeFromConfigFile(string configSectionName);
        /// <summary>
        /// 获取包装的容器实例.
        /// </summary>
        /// <typeparam name="T">包装的容器的类型.</typeparam>
        /// <returns>包装容器的实例.</returns>
        public abstract T GetWrappedContainer<T>();
        /// <summary>
        /// 表示当前的对象容器.
        /// </summary>
        public IObjectContainer Current { get; private set; }

        /// <summary>
        /// 设置对象容器.
        /// </summary>
        /// <param name="container"></param>
        public void SetContainer(IObjectContainer container)
        {
            Current = container;
        }

        /// <summary>
        /// 构建容器.
        /// </summary>
        public void Build()
        {
            Current.Build();
        }
        #region 注册
        public void Register<T>(string serviceName = null, LifetimeStyle lifeStyle = LifetimeStyle.Singleton) where T : class
        {
            Current.Register<T>(serviceName, lifeStyle);
        }

        public void Register(Type type, string serviceName = null, LifetimeStyle lifeStyle = LifetimeStyle.Singleton)
        {
            Current.Register(type, serviceName, lifeStyle);
        }

        public void Register<TType, TImpl>(string serviceName, LifetimeStyle lifeStyle) where TType:class
            where TImpl:class,TType
        {
            Current.Register<TType, TImpl>(serviceName, lifeStyle);
        }

        public void Register(Type type, Type impl, string serviceName = null, LifetimeStyle lifeStyle = LifetimeStyle.Singleton)
        {
            Current.Register(type, impl, serviceName, lifeStyle);
        }
        public void RegisterInstance<TService, TImplementer>(TImplementer instance, string serviceName) where TService:class
            where TImplementer:class,TService
        {
            Current.RegisterInstance<TService, TImplementer>(instance, serviceName);
        }
        public bool IsRegistered(Type type)
        {
            return Current.IsRegistered(type);
        }

        public bool IsRegistered<TType>()
        {
            return Current.IsRegistered<TType>();
        }
        #endregion

        #region 解析
        public T Resolve<T>()
        {
            return Current.Resolve<T>();
        }

        public T Resolve<T>(Type type)
        {
            return Current.Resolve<T>(type);
        }

        public T Resolve<T>(params object[] argumentsAsAnonymousType)
        {
            return Current.Resolve<T>(argumentsAsAnonymousType);
        }

        public object Resolve(Type type)
        {
            return Current.Resolve(type);
        }

        public object Resolve(Type type, params object[] argumentsAsAnonymousType)
        {
            return Current.Resolve(type, argumentsAsAnonymousType);
        }
        public bool TryResolve<TService>(out TService instance) where TService : class
        {
            return Current.TryResolve(out instance);
        }

        public bool TryResolve(Type serviceType, out object instance)
        {
            return Current.TryResolve(serviceType, out instance);
        }

        public TService ResolveNamed<TService>(string serviceName) where TService : class
        {
            return Current.ResolveNamed<TService>(serviceName);
        }

        public object ResolveNamed(string serviceName, Type serviceType)
        {
            return Current.ResolveNamed(serviceName, serviceType);
        }

        public bool TryResolveNamed(string serviceName, Type serviceType, out object instance)
        {
            return Current.TryResolveNamed(serviceName, serviceType, out instance);
        }
        public T[] ResolveAll<T>()
        {
            return Current.ResolveAll<T>();
        }

        public T[] ResolveAll<T>(params object[] argumentsAsAnonymousType)
        {
            return Current.ResolveAll<T>(argumentsAsAnonymousType);
        }

        public object[] ResolveAll(Type type)
        {
            return Current.ResolveAll(type);
        }

        public object[] ResolveAll(Type type, params object[] argumentsAsAnonymousType)
        {
            return Current.ResolveAll(type,argumentsAsAnonymousType);
        }
        #endregion
        public void Dispose()
        {
            Current.Dispose();
        }
        private object GetProxyObject(Type targetType, object targetObject)
        {
            IInterceptor[] interceptors = AppRuntime.Instance.CurrentApplication.Interceptors.ToArray();

            if (interceptors == null ||
                interceptors.Length == 0)
                return targetObject;

            if (targetType.IsInterface)
            {
                object obj = null;
                ProxyGenerationOptions proxyGenerationOptionsForInterface = new ProxyGenerationOptions();
                proxyGenerationOptionsForInterface.Selector = interceptorSelector;
                Type targetObjectType = targetObject.GetType();
                if (targetObjectType.IsDefined(typeof(BaseTypeForInterfaceProxyAttribute), false))
                {
                    BaseTypeForInterfaceProxyAttribute baseTypeForIPAttribute = targetObjectType.GetCustomAttributes(typeof(BaseTypeForInterfaceProxyAttribute), false)[0] as BaseTypeForInterfaceProxyAttribute;
                    proxyGenerationOptionsForInterface.BaseTypeForInterfaceProxy = baseTypeForIPAttribute.BaseType;
                }
                if (targetObjectType.IsDefined(typeof(AdditionalInterfaceToProxyAttribute), false))
                {
                    List<Type> intfTypes = targetObjectType.GetCustomAttributes(typeof(AdditionalInterfaceToProxyAttribute), false)
                                                           .Select(p =>
                                                           {
                                                               AdditionalInterfaceToProxyAttribute attrib = p as AdditionalInterfaceToProxyAttribute;
                                                               return attrib.InterfaceType;
                                                           }).ToList();
                    obj = proxyGenerator.CreateInterfaceProxyWithTarget(targetType, intfTypes.ToArray(), targetObject, proxyGenerationOptionsForInterface, interceptors);
                }
                else
                    obj = proxyGenerator.CreateInterfaceProxyWithTarget(targetType, targetObject, proxyGenerationOptionsForInterface, interceptors);
                return obj;
            }
            else
                return proxyGenerator.CreateClassProxyWithTarget(targetType, targetObject, proxyGenerationOptions, interceptors);
        }

        
    }
}
