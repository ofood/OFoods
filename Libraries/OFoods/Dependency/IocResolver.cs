using System;
using OFoods.Application;
namespace OFoods.Dependency
{
    public sealed class IocResolver : IIocResolver
    {
        private readonly IObjectContainer objectContainer = AppRuntime.Instance.CurrentApplication.ObjectContainer;
        private static readonly IocResolver instance = new IocResolver();
        /// <summary>
        /// 实例化解析器
        /// </summary>
        public static IocResolver Instance
        {
            get { return instance; }
        }
        /// <summary>
        /// 初始化<c>IocResolver</c>类的新实例.
        /// </summary>
        private IocResolver() { }

        public T Resolve<T>()
        {
            return objectContainer.Resolve<T>();
        }

        public T Resolve<T>(Type type)
        {
            return objectContainer.Resolve<T>(type);
        }

        public T Resolve<T>(params object[] argumentsAsAnonymousType)
        {
            return objectContainer.Resolve<T>(argumentsAsAnonymousType);
        }

        public object Resolve(Type type)
        {
            return objectContainer.Resolve(type);
        }

        public object Resolve(Type type, params object[] argumentsAsAnonymousType)
        {
            return objectContainer.Resolve(type,argumentsAsAnonymousType);
        }
        public TService ResolveNamed<TService>(string serviceName) where TService : class
        {
            return objectContainer.ResolveNamed<TService>(serviceName);
        }

        public object ResolveNamed(string serviceName, Type serviceType)
        {
            return objectContainer.ResolveNamed(serviceName,serviceType);
        }

        public bool TryResolve<TService>(out TService instance) where TService : class
        {
            return objectContainer.TryResolve<TService>(out instance);
        }

        public bool TryResolve(Type serviceType, out object instance)
        {
            return objectContainer.TryResolve(serviceType,out instance);
        }

        public bool TryResolveNamed(string serviceName, Type serviceType, out object instance)
        {
            return objectContainer.TryResolveNamed(serviceName,serviceType,out instance);
        }

        public T[] ResolveAll<T>()
        {
            return objectContainer.ResolveAll<T>();
        }

        public T[] ResolveAll<T>(params object[] argumentsAsAnonymousType)
        {
            return objectContainer.ResolveAll<T>(argumentsAsAnonymousType);
        }

        public object[] ResolveAll(Type type)
        {
            return objectContainer.ResolveAll(type);
        }

        public object[] ResolveAll(Type type, params object[] argumentsAsAnonymousType)
        {
            return objectContainer.ResolveAll(type,argumentsAsAnonymousType);
        }
    }
}
