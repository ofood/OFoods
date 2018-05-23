using OFoods.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using Autofac.Core;

namespace OFoods.ObjectContainers.Autofac
{
    /// <summary>
    /// Autofac实现IObjectContainer.
    /// </summary>
    public class AutofacObjectContainer: IObjectContainer
    {
        private readonly ContainerBuilder _containerBuilder;
        private IContainer _container;
        #region 构造函数
        /// <summary>
        /// 默认构造函数.
        /// </summary>
        public AutofacObjectContainer() : this(new ContainerBuilder())
        {

        }
        /// <summary>
        /// 参数化的构造函数.
        /// </summary>
        public AutofacObjectContainer(ContainerBuilder containerBuilder)
        {
            _containerBuilder = containerBuilder;
        }
        #endregion

        /// <summary>
        /// 表示内部autofac容器生成器.
        /// </summary>
        public ContainerBuilder ContainerBuilder
        {
            get
            {
                return _containerBuilder;
            }
        }
        /// <summary>
        /// 表示内部的autofac容器.
        /// </summary>
        public IContainer Container
        {
            get
            {
                return _container;
            }
        }
        /// <summary>
        /// 构建容器.
        /// </summary>
        public void Build()
        {
            _container = _containerBuilder.Build();
        }
        public  void InitializeFromConfigFile(string configPath)
        {
            var config = new ConfigurationBuilder();
            config.AddXmlFile(configPath);
            var module = new ConfigurationModule(config.Build());
            ContainerBuilder.RegisterModule(module);
        }
        #region 注册服务
        /// <summary>
        /// 注册一个泛型实现类型.
        /// </summary>
        /// <param name="implementationType">实现类型.</param>
        /// <param name="serviceName">服务名.</param>
        /// <param name="life">实现类型的生命周期.</param>
        public void Register(Type implementationType, string serviceName = null, LifetimeStyle life = LifetimeStyle.Singleton)
        {
            if (implementationType.IsGenericType)
            {
                var registrationBuilder = _containerBuilder.RegisterGeneric(implementationType);
                if (serviceName != null)
                {
                    registrationBuilder.Named(serviceName, implementationType);
                }
                if (life == LifetimeStyle.Singleton)
                {
                    registrationBuilder.SingleInstance();
                }
            }
            else
            {
                var registrationBuilder = _containerBuilder.RegisterType(implementationType);
                if (serviceName != null)
                {
                    registrationBuilder.Named(serviceName, implementationType);
                }
                if (life == LifetimeStyle.Singleton)
                {
                    registrationBuilder.SingleInstance();
                }
            }
        }
        /// <summary>
        /// 将实施类型注册为服务实现.
        /// </summary>
        /// <param name="serviceType">服务类型.</param>
        /// <param name="implementationType">实现类型.</param>
        /// <param name="serviceName">服务名.</param>
        /// <param name="life">实现类型的生命周期.</param>
        public void Register(Type serviceType, Type implementationType, string serviceName = null, LifetimeStyle life = LifetimeStyle.Singleton)
        {
            if (implementationType.IsGenericType)
            {
                var registrationBuilder = _containerBuilder.RegisterGeneric(implementationType).As(serviceType);
                if (serviceName != null)
                {
                    registrationBuilder.Named(serviceName, implementationType);
                }
                if (life == LifetimeStyle.Singleton)
                {
                    registrationBuilder.SingleInstance();
                }
            }
            else
            {
                var registrationBuilder = _containerBuilder.RegisterType(implementationType).As(serviceType);
                if (serviceName != null)
                {
                    registrationBuilder.Named(serviceName, serviceType);
                }
                if (life == LifetimeStyle.Singleton)
                {
                    registrationBuilder.SingleInstance();
                }
            }
        }
        /// <summary>
        /// 将实现类型注册为服务实现.
        /// </summary>
        /// <typeparam name="TService">服务类型.</typeparam>
        /// <typeparam name="TImplementer">实现类型.</typeparam>
        /// <param name="serviceName">服务名称.</param>
        /// <param name="life">实现类型的生命周期.</param>
        public void Register<TService, TImplementer>(string serviceName = null, LifetimeStyle life = LifetimeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService
        {
            var registrationBuilder = _containerBuilder.RegisterType<TImplementer>().As<TService>();
            if (serviceName != null)
            {
                registrationBuilder.Named<TService>(serviceName);
            }
            if (life == LifetimeStyle.Singleton)
            {
                registrationBuilder.SingleInstance();
            }
        }
        /// <summary>
        /// 将实现类型实例注册为服务实现.
        /// </summary>
        /// <typeparam name="TService">服务类型.</typeparam>
        /// <typeparam name="TImplementer">实现类型.</typeparam>
        /// <param name="instance">实现类型实例.</param>
        /// <param name="serviceName">服务名称.</param>
        public void RegisterInstance<TService, TImplementer>(TImplementer instance, string serviceName = null)
            where TService : class
            where TImplementer : class, TService
        {
            var registrationBuilder = _containerBuilder.RegisterInstance(instance).As<TService>().SingleInstance();
            if (serviceName != null)
            {
                registrationBuilder.Named<TService>(serviceName);
            }
        }
        public void Register<T>(string serviceName = null, LifetimeStyle lifeStyle = LifetimeStyle.Singleton) where T : class
        {
            Register(typeof(T),serviceName,lifeStyle);
        }

        public bool IsRegistered(Type type)
        {
            return Container.IsRegistered(type);
        }

        public bool IsRegistered<TType>()
        {
            return Container.IsRegistered<TType>();
        }
        #endregion
        #region 解析服务
        /// <summary>
        /// 解析服务.
        /// </summary>
        /// <typeparam name="TService">服务类型.</typeparam>
        /// <returns>提供服务的组件实例.</returns>
        public TService Resolve<TService>()
        {
            return Container.Resolve<TService>();
        }
        /// <summary>
        /// 解析服务.
        /// </summary>
        /// <param name="serviceType">服务类型.</param>
        /// <returns>提供服务的组件实例.</returns>
        public object Resolve(Type serviceType)
        {
            return Container.Resolve(serviceType);
        }
        /// <summary>
        /// 尝试从容器中检索服务.
        /// </summary>
        /// <typeparam name="TService">要解析的服务类型.</typeparam>
        /// <param name="instance">生成的组件实例提供服务，或默认（TService）.</param>
        /// <returns>如果提供服务的组件可用，则为true.</returns>
        public bool TryResolve<TService>(out TService instance) where TService : class
        {
            return Container.TryResolve(out instance);
        }
        /// <summary>尝试从容器中检索服务.
        /// </summary>
        /// <param name="serviceType">要解析的服务类型.</param>
        /// <param name="instance">生成的组件实例提供服务，或者为null.</param>
        /// <returns>如果提供服务的组件可用，则为true.</returns>
        public bool TryResolve(Type serviceType, out object instance)
        {
            return Container.TryResolve(serviceType, out instance);
        }
        /// <summary>
        /// 解析服务.
        /// </summary>
        /// <typeparam name="TService">服务类型.</typeparam>
        /// <param name="serviceName">服务名.</param>
        /// <returns>提供服务的组件实例.</returns>
        public TService ResolveNamed<TService>(string serviceName) where TService : class
        {
            return Container.ResolveNamed<TService>(serviceName);
        }
        /// <summary>
        /// 解析服务.
        /// </summary>
        /// <param name="serviceName">服务名.</param>
        /// <param name="serviceType">服务类型.</param>
        /// <returns>提供服务的组件实例.</returns>
        public object ResolveNamed(string serviceName, Type serviceType)
        {
            return Container.ResolveNamed(serviceName, serviceType);
        }
        /// <summary>尝试从容器中检索服务.
        /// </summary>
        /// <param name="serviceName">要解析的服务的名称.</param>
        /// <param name="serviceType">要解析的服务的类型.</param>
        /// <param name="instance">生成的组件实例提供服务，或者为null.</param>
        /// <returns>如果提供服务的组件可用，则为true.</returns>
        public bool TryResolveNamed(string serviceName, Type serviceType, out object instance)
        {
            return Container.TryResolveNamed(serviceName, serviceType, out instance);
        }

        public T Resolve<T>(Type type)
        {
            return (T)Container.Resolve(type);
        }

        public T Resolve<T>(params object[] argumentsAsAnonymousType)
        {
            return (T)Container.Resolve(typeof(T),new TypedParameter(argumentsAsAnonymousType.GetType(), argumentsAsAnonymousType));
        }

        public object Resolve(Type type, params object[] argumentsAsAnonymousType)
        {
            return Container.Resolve(type,new TypedParameter(argumentsAsAnonymousType.GetType(), argumentsAsAnonymousType));
        }
        public T[] ResolveAll<T>()
        {
            return Container.ResolveAll<T>();
        }

        public T[] ResolveAll<T>(params object[] argumentsAsAnonymousType)
        {
            return Container.ResolveAll<T>(argumentsAsAnonymousType);
        }

        public object[] ResolveAll(Type type)
        {
            return Container.ResolveAll(type);
        }

        public object[] ResolveAll(Type type,params object[] argumentsAsAnonymousType)
        {
            return Container.ResolveAll(type,argumentsAsAnonymousType);
        }
        public void Dispose()
        {
            Container.Dispose();
        }

        
        #endregion
    }
}
