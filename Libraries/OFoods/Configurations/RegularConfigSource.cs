using System;
using OFoods.Configurations.Elements;
using System.Reflection;
using OFoods.Utility.Extensions;
using Castle.DynamicProxy;

namespace OFoods.Configurations
{
    public sealed class RegularConfigSource:IConfigSource
    {
        private readonly OFoodsConfigSection config;
        /// <summary>
        /// 初始化<c> RegularConfigSource </ c>类的新实例.
        /// </summary>
        public RegularConfigSource()
        {
            config = new OFoodsConfigSection();
            config.Application = new ApplicationElement();
            config.Exceptions = new ExceptionElementCollection();
            config.Generators = new GeneratorElement();
            config.Generators.IdentityGenerator = new IdentityGeneratorElement();
            config.Generators.SequenceGenerator = new SequenceGeneratorElement();
            config.Handlers = new HandlerElementCollection();
            config.ObjectContainer = new ObjectContainerElement();
            config.Serializers = new SerializerElement();
            config.Serializers.EventSerializer = new EventSerializerElement();
            config.Serializers.SnapshotSerializer = new SnapshotSerializerElement();
            config.Interception = new InterceptionElement();
            config.Interception.Contracts = new InterceptContractElementCollection();
            config.Interception.Interceptors = new InterceptorElementCollection();
        }
        /// <summary>
        /// 获取或设置<see cref ="OFoods.Application.IApp"/>实例的类型.
        /// </summary>
        public Type Application
        {
            get { return Type.GetType(config.Application.Provider); }
            set { config.Application.Provider = value.AssemblyQualifiedName; }
        }
        /// <summary>
        /// 获取或设置标识生成器的类型.
        /// </summary>
        public Type IdentityGenerator
        {
            get { return Type.GetType(config.Generators.IdentityGenerator.Provider);  }
            set { config.Generators.IdentityGenerator.Provider = value.AssemblyQualifiedName; }
        }
        /// <summary>
        /// 获取或设置序列生成器的类型.
        /// </summary>
        public Type SequenceGenerator
        {
            get { return Type.GetType(config.Generators.SequenceGenerator.Provider); }
            set { config.Generators.SequenceGenerator.Provider = value.AssemblyQualifiedName; }
        }
        /// <summary>
        /// 获取或设置对象容器的类型.
        /// </summary>
        public Type ObjectContainer
        {
            get { return Type.GetType(config.ObjectContainer.Provider); }
            set { config.ObjectContainer.Provider = value.AssemblyQualifiedName; }
        }
        /// <summary>
        /// 获取或设置一个<see cref ="System.Boolean"/>值，该值指示对象容器是否应由应用程序/ Web配置文件初始化.
        /// </summary>
        public bool InitObjectContainerFromConfigFile
        {
            get { return config.ObjectContainer.InitFromConfigFile; }
            set { config.ObjectContainer.InitFromConfigFile = value; }
        }
        /// <summary>
        /// 获取或设置应用程序/ Web配置文件中的ConfigurationSection的名称，该对象容器应与之初始化.
        /// </summary>
        public string ObjectContainerSectionName
        {
            get { return config.ObjectContainer.SectionName; }
            set { config.ObjectContainer.SectionName = value; }
        }
        /// <summary>
        /// 获取或设置事件序列化程序的类型.
        /// </summary>
        public Type EventSerializer
        {
            get { return Type.GetType(config.Serializers.EventSerializer.Provider); }
            set { config.Serializers.EventSerializer.Provider = value.AssemblyQualifiedName; }
        }
        /// <summary>
        /// 获取或设置快照序列化程序的类型.
        /// </summary>
        public Type SnapshotSerializer
        {
            get { return Type.GetType(config.Serializers.SnapshotSerializer.Provider); }
            set { config.Serializers.SnapshotSerializer.Provider = value.AssemblyQualifiedName; }
        }
        #region 公共方法
        /// <summary>
        /// 向ConfigSource添加命令或事件处理程序.
        /// </summary>
        /// <param name="name">要添加的处理程序的名称.</param>
        /// <param name="kind"><see cref ="HandlerKind"/>值表示消息的类型.</param>
        /// <param name="sourceType">指示处理程序存在位置的<see cref ="HandlerSourceType"/>值。 这可以是一个类型或一个程序集.</param>
        /// <param name="source">源的标识符，对于<c> sourceType == HandlerSourceType.Type </ c>，
        /// 这个值应该是类型的名称; 对于<c> sourceType == HandlerSourceType.Assembly </ c>，
        /// 这个值应该是程序集的名称.</param>
        public void AddHandler(string name, HandlerKind kind, HandlerSourceType sourceType, string source)
        {
            if (config.Handlers == null)
                config.Handlers = new HandlerElementCollection();
            foreach (HandlerElement he in config.Handlers)
            {
                if ((he.Name == name) || (he.Kind == kind && he.SourceType == sourceType && he.Source == source))
                    return;
            }
            config.Handlers.Add(new HandlerElement
            {
                Name = name,
                Kind = kind,
                SourceType = sourceType,
                Source = source
            });
        }
        /// <summary>
        /// 向ConfigSource添加一个异常定义.
        /// </summary>
        /// <param name="exceptionType">要保护的例外类型.</param>
        /// <param name="behavior">异常处理行为.</param>
        public void AddException(Type exceptionType, ExceptionHandlingBehavior behavior = ExceptionHandlingBehavior.Direct)
        {
            if (config.Exceptions == null)
                config.Exceptions = new ExceptionElementCollection();
            foreach (ExceptionElement ee in config.Exceptions)
            {
                if (ee.Type == exceptionType.AssemblyQualifiedName && ee.Behavior == behavior)
                    return;
            }
            config.Exceptions.Add(new ExceptionElement
            {
                Type = exceptionType.AssemblyQualifiedName,
                Behavior = behavior,
                Handlers = new ExceptionHandlerElementCollection()
            });
        }
        /// <summary>
        /// 向ConfigSource添加一个异常处理程序定义.
        /// </summary>
        /// <param name="exceptionType">正在处理的异常的类型.</param>
        /// <param name="handlerType">异常处理程序的类型.</param>
        public void AddExceptionHandler(Type exceptionType, Type handlerType)
        {
            foreach (ExceptionElement exceptionElement in config.Exceptions)
            {
                if (exceptionElement.Type.Equals(exceptionType.AssemblyQualifiedName))
                {
                    if (exceptionElement.Handlers == null)
                        exceptionElement.Handlers = new ExceptionHandlerElementCollection();
                    foreach (ExceptionHandlerElement exceptionHandlerElement in exceptionElement.Handlers)
                    {
                        if (exceptionHandlerElement.Type.Equals(handlerType.AssemblyQualifiedName))
                            return;
                    }
                    exceptionElement.Handlers.Add(new ExceptionHandlerElement
                    {
                        Type = handlerType.AssemblyQualifiedName
                    });
                }
            }
        }

        /// <summary>
        /// 向当前的ConfigSource添加一个拦截器.
        /// </summary>
        /// <param name="name">拦截器的名称.</param>
        /// <param name="interceptorType">拦截器的类型.</param>
        public void AddInterceptor(string name, Type interceptorType)
        {
            if (!typeof(IInterceptor).IsAssignableFrom(interceptorType))
                throw new OFoodsException("类型'{0}'不是拦截器.", interceptorType);

            if (config.Interception == null)
                config.Interception = new InterceptionElement();
            if (config.Interception.Interceptors == null)
                config.Interception.Interceptors = new InterceptorElementCollection();
            foreach (InterceptorElement interceptor in config.Interception.Interceptors)
            {
                if (interceptor.Name.Equals(name) || interceptor.Type.Equals(interceptorType.AssemblyQualifiedName))
                    return;
            }
            InterceptorElement interceptorAdd = new InterceptorElement();
            interceptorAdd.Name = name;
            interceptorAdd.Type = interceptorType.AssemblyQualifiedName;
            config.Interception.Interceptors.Add(interceptorAdd);
        }
        /// <summary>
        /// 为指定的契约和方法添加拦截参考.
        /// </summary>
        /// <param name="contractType">契约类型.</param>
        /// <param name="method">方法.</param>
        /// <param name="name">拦截参考的名称.</param>
        public void AddInterceptorRef(Type contractType, MethodInfo method, string name)
        {
            if (config.Interception != null)
            {
                if (config.Interception.Contracts != null)
                {
                    foreach (InterceptContractElement interceptContract in config.Interception.Contracts)
                    {
                        if (interceptContract.Type.Equals(contractType.AssemblyQualifiedName))
                        {
                            if (interceptContract.Methods != null)
                            {
                                foreach (InterceptMethodElement interceptMethod in interceptContract.Methods)
                                {
                                    if (interceptMethod.Signature.Equals(method.GetSignature()))
                                    {
                                        if (interceptMethod.InterceptorRefs != null)
                                        {
                                            foreach (InterceptorRefElement interceptorRef in interceptMethod.InterceptorRefs)
                                            {
                                                if (interceptorRef.Name.Equals(name))
                                                    return;
                                            }
                                            interceptMethod.InterceptorRefs.Add(new InterceptorRefElement { Name = name });
                                        }
                                        else
                                        {
                                            interceptMethod.InterceptorRefs = new InterceptorRefElementCollection();
                                            interceptMethod.InterceptorRefs.Add(new InterceptorRefElement { Name = name });
                                        }
                                        return;
                                    }
                                }
                                InterceptMethodElement interceptMethodAdd = new InterceptMethodElement();
                                interceptMethodAdd.Signature = method.GetSignature();
                                interceptMethodAdd.InterceptorRefs = new InterceptorRefElementCollection();
                                interceptMethodAdd.InterceptorRefs.Add(new InterceptorRefElement { Name = name });
                                interceptContract.Methods.Add(interceptMethodAdd);
                            }
                            else
                            {
                                interceptContract.Methods = new InterceptMethodElementCollection();
                                InterceptMethodElement interceptMethodAdd = new InterceptMethodElement();
                                interceptMethodAdd.Signature = method.GetSignature();
                                interceptMethodAdd.InterceptorRefs = new InterceptorRefElementCollection();
                                interceptMethodAdd.InterceptorRefs.Add(new InterceptorRefElement { Name = name });
                                interceptContract.Methods.Add(interceptMethodAdd);
                            }
                            return;
                        }
                    }
                    InterceptContractElement interceptContractAdd = new InterceptContractElement();
                    interceptContractAdd.Type = contractType.AssemblyQualifiedName;
                    interceptContractAdd.Methods = new InterceptMethodElementCollection();
                    InterceptMethodElement interceptMethodAddToContract = new InterceptMethodElement();
                    interceptMethodAddToContract.Signature = method.GetSignature();
                    interceptMethodAddToContract.InterceptorRefs = new InterceptorRefElementCollection();
                    interceptMethodAddToContract.InterceptorRefs.Add(new InterceptorRefElement { Name = name });
                    interceptContractAdd.Methods.Add(interceptMethodAddToContract);
                    config.Interception.Contracts.Add(interceptContractAdd);
                }
                else
                {
                    this.config.Interception.Contracts = new InterceptContractElementCollection();
                    InterceptContractElement interceptContractAdd = new InterceptContractElement();
                    interceptContractAdd.Type = contractType.AssemblyQualifiedName;
                    interceptContractAdd.Methods = new InterceptMethodElementCollection();
                    InterceptMethodElement interceptMethodAddToContract = new InterceptMethodElement();
                    interceptMethodAddToContract.Signature = method.GetSignature();
                    interceptMethodAddToContract.InterceptorRefs = new InterceptorRefElementCollection();
                    interceptMethodAddToContract.InterceptorRefs.Add(new InterceptorRefElement { Name = name });
                    interceptContractAdd.Methods.Add(interceptMethodAddToContract);
                    config.Interception.Contracts.Add(interceptContractAdd);
                }
            }
            else
            {
                config.Interception = new InterceptionElement();
                config.Interception.Contracts = new InterceptContractElementCollection();
                InterceptContractElement interceptContractAdd = new InterceptContractElement();
                interceptContractAdd.Type = contractType.AssemblyQualifiedName;
                interceptContractAdd.Methods = new InterceptMethodElementCollection();
                InterceptMethodElement interceptMethodAddToContract = new InterceptMethodElement();
                interceptMethodAddToContract.Signature = method.GetSignature();
                interceptMethodAddToContract.InterceptorRefs = new InterceptorRefElementCollection();
                interceptMethodAddToContract.InterceptorRefs.Add(new InterceptorRefElement { Name = name });
                interceptContractAdd.Methods.Add(interceptMethodAddToContract);
                config.Interception.Contracts.Add(interceptContractAdd);
            }
        }
        #endregion

        #region IConfigSource 成员
        /// <summary>
        /// 获取<see cref ="OFoodsConfigSection"/>类的实例.
        /// </summary>
        public OFoodsConfigSection Config
        {
            get { return config; }
        }

        #endregion
    }
}
