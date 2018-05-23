using Castle.DynamicProxy;
using OFoods.Application;
using OFoods.Configurations.Elements;
using OFoods.Dependency;
using OFoods.Exceptions;
using OFoods.Generators;
using System;
using System.Reflection;

namespace OFoods.Configurations.Fluent
{
    /// <summary>
    /// 表示扩展方法提供程序，该提供程序为OFood框架配置的Fluent API提供例程.
    /// </summary>
    public static class Extensions
    {
        #region IFoodConfigurator扩展器
        /// <summary>
        /// 使用指定的应用程序对象配置OFood框架.
        /// </summary>
        /// <typeparam name="TApplication">应用程序对象的类型.</typeparam>
        /// <param name="configurator"><see cref ="IOFoodConfigurator"/>要扩展的实例.</param>
        /// <returns>The <see cref="IApplicationConfigurator"/> instance.</returns>
        public static IApplicationConfigurator WithApplication<TApplication>(this IOFoodsConfigurator configurator)
            where TApplication : IApp
        {
            return new ApplicationConfigurator(configurator, typeof(TApplication));
        }
        /// <summary>
        /// 使用默认应用程序实例配置OFoods框架.
        /// </summary>
        /// <param name="configurator"><see cref ="IApworksConfigurator"/>要扩展的实例.</param>
        /// <returns><see cref ="IApplicationConfigurator"/>实例.</returns>
        public static IApplicationConfigurator WithDefaultApplication(this IOFoodsConfigurator configurator)
        {
            return WithApplication<App>(configurator);
        }
        /// <summary>
        /// 使用默认设置配置OFood框架：默认应用程序实例，默认标识生成器和默认序列生成器.
        /// </summary>
        /// <param name="configurator">The <see cref="IApworksConfigurator"/> instance to be extended.</param>
        /// <returns>The <see cref="ISequenceGeneratorConfigurator"/> instance.</returns>
        public static ISequenceGeneratorConfigurator WithDefaultSettings(this IOFoodsConfigurator configurator)
        {
            return WithDefaultSequenceGenerator(
                WithDefaultIdentityGenerator(
                WithDefaultApplication(configurator)));
        }
        #endregion

        #region IAppConfigurator扩展器
        /// <summary>
        /// 使用指定的标识生成器配置OFood框架.
        /// </summary>
        /// <typeparam name="TIdentityGenerator">框架使用的标识生成器的类型.</typeparam>
        /// <param name="configurator"><see cref ="IApplicationConfigurator"/>要扩展的实例.</param>
        /// <returns><see cref ="IIdentityGeneratorConfigurator"/>实例.</returns>
        public static IIdentityGeneratorConfigurator WithIdentityGenerator<TIdentityGenerator>(this IApplicationConfigurator configurator)
            where TIdentityGenerator : IIdentityGenerator
        {
            return new IdentityGeneratorConfigurator(configurator, typeof(TIdentityGenerator));
        }
        /// <summary>
        /// 使用默认标识生成器配置OFood框架.
        /// </summary>
        /// <param name="configurator"><see cref ="IApplicationConfigurator"/>要扩展的实例.</param>
        /// <returns><see cref ="IIdentityGeneratorConfigurator"/>实例.</returns>
        public static IIdentityGeneratorConfigurator WithDefaultIdentityGenerator(this IApplicationConfigurator configurator)
        {
            return WithIdentityGenerator<SequentialIdentityGenerator>(configurator);
        }
        #endregion

        #region IIdentityGeneratorConfigurator 扩展
        /// <summary>
        /// 使用指定的序列生成器配置OFood框架.
        /// </summary>
        /// <typeparam name="TSequenceGenerator">框架使用的序列生成器的类型.</typeparam>
        /// <param name="configurator"><see cref ="IIdentityGeneratorConfigurator"/>要扩展的实例.</param>
        /// <returns> <see cref="ISequenceGeneratorConfigurator"/> 实例.</returns>
        public static ISequenceGeneratorConfigurator WithSequenceGenerator<TSequenceGenerator>(this IIdentityGeneratorConfigurator configurator)
            where TSequenceGenerator : ISequenceGenerator
        {
            return new SequenceGeneratorConfigurator(configurator, typeof(TSequenceGenerator));
        }
        /// <summary>
        /// 使用默认序列生成器配置OFood框架.
        /// </summary>
        /// <param name="configurator"> <see cref="IIdentityGeneratorConfigurator"/> 要扩展的实例.</param>
        /// <returns> <see cref="ISequenceGeneratorConfigurator"/> 实例.</returns>
        public static ISequenceGeneratorConfigurator WithDefaultSequenceGenerator(this IIdentityGeneratorConfigurator configurator)
        {
            return WithSequenceGenerator<SequentialIdentityGenerator>(configurator);
        }
        #endregion

        #region ISequenceGeneratorConfigurator 扩展
        /// <summary>
        /// 向OFood框架添加消息处理程序。 （此操作仅适用于CQRS架构）.
        /// </summary>
        /// <param name="configurator"> <see cref="ISequenceGeneratorConfigurator"/> 要被扩展的实例.</param>
        /// <param name="handlerKind">指定处理程序类型的<see cref ="HandlerKind"/>可以是Command或Event.</param>
        /// <param name="sourceType">指定源的类型的<see cref ="HandlerSourceType"/>可以是程序集或类型.</param>
        /// <param name="source">源名称，如果<paramref name ="sourceType"/>是Assembly，则源名称应该是程序集全名，
        /// 如果<paramref name ="sourceType"/>是Type，源名称应该是程序集限定名称 方式.</param>
        /// <param name="name">消息处理程序的名称.</param>
        /// <returns> <see cref="IHandlerConfigurator"/> 实例.</returns>
        public static IHandlerConfigurator AddMessageHandler(this ISequenceGeneratorConfigurator configurator, HandlerKind handlerKind, HandlerSourceType sourceType, string source, string name = null)
        {
            if (string.IsNullOrEmpty(name))
                return new HandlerConfigurator(configurator, handlerKind, sourceType, source);
            else
                return new HandlerConfigurator(configurator, name, handlerKind, sourceType, source);
        }
        /// <summary>
        /// 向OFood框架添加一个异常处理程序.
        /// </summary>
        /// <typeparam name="TException">要处理的异常的类型.</typeparam>
        /// <typeparam name="TExceptionHandler">异常处理程序的类型.</typeparam>
        /// <param name="configurator"><see cref ="ISequenceGeneratorConfigurator"/>将被扩展.</param>
        /// <param name="behavior">异常处理行为.</param>
        /// <returns> <see cref="IExceptionHandlerConfigurator"/> 实例.</returns>
        public static IExceptionHandlerConfigurator AddExceptionHandler<TException, TExceptionHandler>(this ISequenceGeneratorConfigurator configurator, ExceptionHandlingBehavior behavior = ExceptionHandlingBehavior.Direct)
            where TException : Exception
            where TExceptionHandler : IExceptionHandler
        {
            return new ExceptionHandlerConfigurator(configurator, typeof(TException), typeof(TExceptionHandler), behavior);
        }
        /// <summary>
        /// 在给定类型的给定方法上注册一个拦截器.
        /// </summary>
        /// <typeparam name="TInterceptor">要注册的拦截器的类型.</typeparam>
        /// <typeparam name="TContract">包含要拦截的方法的类型.</typeparam>
        /// <param name="configurator"> <see cref="ISequenceGeneratorConfigurator"/> 要扩展的实例.</param>
        /// <param name="interceptMethod">要拦截的方法.</param>
        /// <returns> <see cref="IInterceptionConfigurator"/> 实例.</returns>
        public static IInterceptionConfigurator RegisterInterception<TInterceptor, TContract>(this ISequenceGeneratorConfigurator configurator, MethodInfo interceptMethod)
            where TInterceptor : IInterceptor
        {
            return new InterceptionConfigurator(configurator, typeof(TInterceptor), typeof(TContract), interceptMethod);
        }
        /// <summary>
        /// 在给定类型的给定方法上注册一个拦截器.
        /// </summary>
        /// <typeparam name="TInterceptor">要注册的拦截器的类型.</typeparam>
        /// <typeparam name="TContract">包含要拦截的方法的类型.</typeparam>
        /// <param name="configurator"><see cref="ISequenceGeneratorConfigurator"/> 要扩展的实例.</param>
        /// <param name="interceptMethod">要拦截的方法.</param>
        /// <returns> <see cref="IInterceptionConfigurator"/> 实例.</returns>
        public static IInterceptionConfigurator RegisterInterception<TInterceptor, TContract>(this ISequenceGeneratorConfigurator configurator, string interceptMethod)
            where TInterceptor : IInterceptor
        {
            return new InterceptionConfigurator(configurator, typeof(TInterceptor), typeof(TContract), interceptMethod);
        }
        /// <summary>
        /// 使用指定的对象容器来配置OFood框架.
        /// </summary>
        /// <typeparam name="TObjectContainer">框架使用的对象容器的类型.</typeparam>
        /// <param name="configurator"> <see cref="ISequenceGeneratorConfigurator"/> 要扩展的实例.</param>
        /// <param name="initFromConfigFile"><see cref ="Boolean"/>值指示是否应该从配置文件读取容器配置.</param>
        /// <param name="sectionName">配置文件中的部分名称。 当<paramref name ="initFromConfigFile"/>参数设置为true时，必须指定此值.</param>
        /// <returns> <see cref="IObjectContainerConfigurator"/> 实例.</returns>
        public static IObjectContainerConfigurator UsingObjectContainer<TObjectContainer>(this ISequenceGeneratorConfigurator configurator, bool initFromConfigFile = false, string sectionName = null)
            where TObjectContainer : IObjectContainer
        {
            return new ObjectContainerConfigurator(configurator, typeof(TObjectContainer), initFromConfigFile, sectionName);
        }
        #endregion

        #region IHandlerConfigurator 扩展
        /// <summary>
        /// 向OFood框架添加消息处理程序。 （此操作仅适用于CQRS架构）.
        /// </summary>
        /// <param name="configurator"> <see cref="IHandlerConfigurator"/> 要扩展的实例.</param>
        /// <param name="handlerKind">指定处理程序类型的<see cref ="HandlerKind"/>可以是Command或Event.</param>
        /// <param name="sourceType">指定源的类型的<see cref ="HandlerSourceType"/>可以是程序集或类型.</param>
        /// <param name="source">源名称，如果<paramref name ="sourceType"/>是Assembly,则源名称应该是程序集全名，
        /// 如果<paramref name =“sourceType”/>是Type，则源名称应该是程序集限定名称 方式.</param>
        /// <param name="name">消息处理程序的名称.</param>
        /// <returns>The <see cref="IHandlerConfigurator"/> instance.</returns>
        public static IHandlerConfigurator AddMessageHandler(this IHandlerConfigurator configurator, HandlerKind handlerKind, HandlerSourceType sourceType, string source, string name = null)
        {
            if (string.IsNullOrEmpty(name))
                return new HandlerConfigurator(configurator, handlerKind, sourceType, source);
            else
                return new HandlerConfigurator(configurator, name, handlerKind, sourceType, source);
        }
        /// <summary>
        /// 向OFood框架添加一个异常处理程序.
        /// </summary>
        /// <typeparam name="TException">要处理的异常的类型.</typeparam>
        /// <typeparam name="TExceptionHandler">异常处理程序的类型.</typeparam>
        /// <param name="configurator"><see cref ="IHandlerConfigurator"/>将被扩展.</param>
        /// <param name="behavior">异常处理行为.</param>
        /// <returns> <see cref="IExceptionHandlerConfigurator"/> 实例.</returns>
        public static IExceptionHandlerConfigurator AddExceptionHandler<TException, TExceptionHandler>(this IHandlerConfigurator configurator, ExceptionHandlingBehavior behavior = ExceptionHandlingBehavior.Direct)
            where TException : Exception
            where TExceptionHandler : IExceptionHandler
        {
            return new ExceptionHandlerConfigurator(configurator, typeof(TException), typeof(TExceptionHandler), behavior);
        }
        /// <summary>
        /// 在给定类型的给定方法上注册一个拦截器.
        /// </summary>
        /// <typeparam name="TInterceptor">要注册的拦截器的类型.</typeparam>
        /// <typeparam name="TContract">包含要拦截的方法的类型.</typeparam>
        /// <param name="configurator"> <see cref="IHandlerConfigurator"/> 要扩展的实例.</param>
        /// <param name="interceptMethod">要拦截的方法.</param>
        /// <returns> <see cref="IInterceptionConfigurator"/> 实例.</returns>
        public static IInterceptionConfigurator RegisterInterception<TInterceptor, TContract>(this IHandlerConfigurator configurator, MethodInfo interceptMethod)
            where TInterceptor : IInterceptor
        {
            return new InterceptionConfigurator(configurator, typeof(TInterceptor), typeof(TContract), interceptMethod);
        }
        /// <summary>
        /// 在给定类型的给定方法上注册一个拦截器.
        /// </summary>
        /// <typeparam name="TInterceptor">要注册的拦截器的类型.</typeparam>
        /// <typeparam name="TContract">包含要拦截的方法的类型.</typeparam>
        /// <param name="configurator"> <see cref="IHandlerConfigurator"/> 要扩展的实例.</param>
        /// <param name="interceptMethod">要拦截的方法.</param>
        /// <returns> <see cref="IInterceptionConfigurator"/> 实例.</returns>
        public static IInterceptionConfigurator RegisterInterception<TInterceptor, TContract>(this IHandlerConfigurator configurator, string interceptMethod)
            where TInterceptor : IInterceptor
        {
            return new InterceptionConfigurator(configurator, typeof(TInterceptor), typeof(TContract), interceptMethod);
        }
        /// <summary>
        /// 使用指定的对象容器来配置OFood框架.
        /// </summary>
        /// <typeparam name="TObjectContainer">框架使用的对象容器的类型.</typeparam>
        /// <param name="configurator"> <see cref="IHandlerConfigurator"/> 要扩展的实例.</param>
        /// <param name="initFromConfigFile"><see cref ="Boolean"/>值指示是否应该从配置文件读取容器配置.</param>
        /// <param name="sectionName">配置文件中的部分名称。 
        /// 当<paramref name ="initFromConfigFile"/>参数设置为true时，必须指定此值.</param>
        /// <returns> <see cref="IObjectContainerConfigurator"/> 实例.</returns>
        public static IObjectContainerConfigurator UsingObjectContainer<TObjectContainer>(this IHandlerConfigurator configurator, bool initFromConfigFile = false, string sectionName = null)
            where TObjectContainer : IObjectContainer
        {
            return new ObjectContainerConfigurator(configurator, typeof(TObjectContainer), initFromConfigFile, sectionName);
        }
        #endregion

        #region IExceptionHandlerConfigurator 扩展
        /// <summary>
        /// 向OFood框架添加一个异常处理程序.
        /// </summary>
        /// <typeparam name="TException">要处理的异常的类型.</typeparam>
        /// <typeparam name="TExceptionHandler">异常处理程序的类型.</typeparam>
        /// <param name="configurator"> <see cref="IExceptionHandlerConfigurator"/> 要被扩展.</param>
        /// <param name="behavior">异常处理行为.</param>
        /// <returns> <see cref="IExceptionHandlerConfigurator"/> 实例.</returns>
        public static IExceptionHandlerConfigurator AddExceptionHandler<TException, TExceptionHandler>(this IExceptionHandlerConfigurator configurator, ExceptionHandlingBehavior behavior = ExceptionHandlingBehavior.Direct)
            where TException : Exception
            where TExceptionHandler : IExceptionHandler
        {
            return new ExceptionHandlerConfigurator(configurator, typeof(TException), typeof(TExceptionHandler), behavior);
        }
        /// <summary>
        /// 在给定类型的给定方法上注册一个拦截器.
        /// </summary>
        /// <typeparam name="TInterceptor">要注册的拦截器的类型.</typeparam>
        /// <typeparam name="TContract">包含要拦截的方法的类型.</typeparam>
        /// <param name="configurator">The <see cref="IExceptionHandlerConfigurator"/> instance to be extended.</param>
        /// <param name="interceptMethod">The method to be intercepted.</param>
        /// <returns> <see cref="IInterceptionConfigurator"/> 实例.</returns>
        public static IInterceptionConfigurator RegisterInterception<TInterceptor, TContract>(this IExceptionHandlerConfigurator configurator, MethodInfo interceptMethod)
            where TInterceptor : IInterceptor
        {
            return new InterceptionConfigurator(configurator, typeof(TInterceptor), typeof(TContract), interceptMethod);
        }
        /// <summary>
        /// 在给定类型的给定方法上注册一个拦截器.
        /// </summary>
        /// <typeparam name="TInterceptor">要注册的拦截器的类型.</typeparam>
        /// <typeparam name="TContract">包含要拦截的方法的类型.</typeparam>
        /// <param name="configurator"> <see cref="IExceptionHandlerConfigurator"/> 要扩展的实例.</param>
        /// <param name="interceptMethod">要拦截的方法.</param>
        /// <returns> <see cref="IInterceptionConfigurator"/> 实例.</returns>
        public static IInterceptionConfigurator RegisterInterception<TInterceptor, TContract>(this IExceptionHandlerConfigurator configurator, string interceptMethod)
            where TInterceptor : IInterceptor
        {
            return new InterceptionConfigurator(configurator, typeof(TInterceptor), typeof(TContract), interceptMethod);
        }
        /// <summary>
        /// 使用指定的对象容器来配置OFood框架.
        /// </summary>
        /// <typeparam name="TObjectContainer">框架使用的对象容器的类型.</typeparam>
        /// <param name="configurator"> <see cref="IExceptionHandlerConfigurator"/> 要扩展的实例.</param>
        /// <param name="initFromConfigFile"><see cref ="Boolean"/>值指示是否应该从配置文件读取容器配置.</param>
        /// <param name="sectionName">配置文件中的部分名称。 当<paramref name ="initFromConfigFile"/>参数设置为true时，必须指定此值.</param>
        /// <returns> <see cref="IObjectContainerConfigurator"/> 实例.</returns>
        public static IObjectContainerConfigurator UsingObjectContainer<TObjectContainer>(this IExceptionHandlerConfigurator configurator, bool initFromConfigFile = false, string sectionName = null)
            where TObjectContainer : IObjectContainer
        {
            return new ObjectContainerConfigurator(configurator, typeof(TObjectContainer), initFromConfigFile, sectionName);
        }
        #endregion

        #region IInterceptionConfigurator 扩展
        /// <summary>
        /// 在给定类型的给定方法上注册一个拦截器.
        /// </summary>
        /// <typeparam name="TInterceptor">要注册的拦截器的类型.</typeparam>
        /// <typeparam name="TContract">包含要拦截的方法的类型.</typeparam>
        /// <param name="configurator"> <see cref="IInterceptionConfigurator"/> 要扩展的实例.</param>
        /// <param name="interceptMethod">要拦截的方法.</param>
        /// <returns> <see cref="IInterceptionConfigurator"/> 实例.</returns>
        public static IInterceptionConfigurator RegisterInterception<TInterceptor, TContract>(this IInterceptionConfigurator configurator, MethodInfo interceptMethod)
            where TInterceptor : IInterceptor
        {
            return new InterceptionConfigurator(configurator, typeof(TInterceptor), typeof(TContract), interceptMethod);
        }
        /// <summary>
        /// 在给定类型的给定方法上注册一个拦截器.
        /// </summary>
        /// <typeparam name="TInterceptor">要注册的拦截器的类型.</typeparam>
        /// <typeparam name="TContract">包含要拦截的方法的类型.</typeparam>
        /// <param name="configurator"> <see cref="IInterceptionConfigurator"/> 要扩展的实例.</param>
        /// <param name="interceptMethod">要拦截的方法.</param>
        /// <returns> <see cref="IInterceptionConfigurator"/> 实例.</returns>
        public static IInterceptionConfigurator RegisterInterception<TInterceptor, TContract>(this IInterceptionConfigurator configurator, string interceptMethod)
            where TInterceptor : IInterceptor
        {
            return new InterceptionConfigurator(configurator, typeof(TInterceptor), typeof(TContract), interceptMethod);
        }

        /// <summary>
        /// 使用指定的对象容器来配置OFood框架.
        /// </summary>
        /// <typeparam name="TObjectContainer">框架使用的对象容器的类型.</typeparam>
        /// <param name="configurator"> <see cref="IInterceptionConfigurator"/> 要扩展的实例.</param>
        /// <param name="initFromConfigFile"><see cref ="Boolean"/>值指示是否应该从配置文件读取容器配置.</param>
        /// <param name="sectionName">配置文件中的部分名称。 当<paramref name ="initFromConfigFile"/>参数设置为true时，必须指定此值.</param>
        /// <returns> <see cref="IObjectContainerConfigurator"/> 实例.</returns>
        public static IObjectContainerConfigurator UsingObjectContainer<TObjectContainer>(this IInterceptionConfigurator configurator, bool initFromConfigFile = false, string sectionName = null)
            where TObjectContainer : IObjectContainer
        {
            return new ObjectContainerConfigurator(configurator, typeof(TObjectContainer), initFromConfigFile, sectionName);
        }
        #endregion

        #region IObjectContainerConfigurator
        /// <summary>
        /// 创建<see cref ="IApp"/>实例.
        /// </summary>
        /// <param name="configurator"><see cref ="IObjectContainerConfigurator"/>要扩展的实例.</param>
        /// <returns> <see cref="IApp"/> 实例.</returns>
        public static IApp Create(this IObjectContainerConfigurator configurator)
        {
            var configSource = configurator.Configure();
            var appInstance = AppRuntime.Create(configSource);
            return appInstance;
        }
        /// <summary>
        /// 创建<see cref ="IApp"/>实例.
        /// </summary>
        /// <param name="configurator"><see cref ="IObjectContainerConfigurator"/>要扩展的实例.</param>
        /// <param name="initializer">应用程序初始化程序.</param>
        /// <returns><see cref="IApp"/> 实例.</returns>
        public static IApp Create(this IObjectContainerConfigurator configurator, EventHandler<AppInitEventArgs> initializer)
        {
            var appInstance = Create(configurator);
            var tmp = initializer;
            if (tmp != null)
                appInstance.Initialize += tmp;
            return appInstance;
        }
        #endregion

        #region AppRuntime Instance 扩展
        /// <summary>
        /// 配置OFood框架.
        /// </summary>
        /// <param name="appRuntime"><see cref ="AppRuntime"/>的实例将被扩展.</param>
        /// <returns>包含OFood框架配置器的<see cref ="IFoodsConfigurator"/>实例.</returns>
        public static IOFoodsConfigurator ConfigureApworks(this AppRuntime appRuntime)
        {
            return new OFoodsConfigurator();
        }
        #endregion
    }
}
