using System;
using System.Collections.Generic;
using System.Linq;
using OFoods.Application;
using OFoods.Configurations;
using OFoods.Configurations.Elements;

namespace OFoods.Exceptions
{
    /// <summary>
    /// 表示处理和处理异常的异常管理器.
    /// </summary>
    public sealed class ExceptionManager
    {
        #region 私有字段
        private static readonly ExceptionManager instance = new ExceptionManager();
        private readonly Dictionary<Type, ExceptionConfigItem> handlersOrig = new Dictionary<Type, ExceptionConfigItem>();
        private readonly Dictionary<Type, List<IExceptionHandler>> handlersResponsibilityChain = new Dictionary<Type, List<IExceptionHandler>>();
        #endregion

        #region 构造函数
        static ExceptionManager() { }
        private ExceptionManager()
        {
            try
            {
                OFoodsConfigSection config = AppRuntime.Instance.CurrentApplication.ConfigSource.Config;
                if (config.Exceptions != null &&
                    config.Exceptions.Count > 0)
                {
                    ExceptionElementCollection exceptionElementCollection = config.Exceptions;
                    foreach (ExceptionElement exceptionElement in exceptionElementCollection)
                    {
                        Type exceptionType = Type.GetType(exceptionElement.Type);
                        if (exceptionType == null)
                            continue;
                        
                        if (exceptionType.IsAbstract ||
                            !typeof(Exception).IsAssignableFrom(exceptionType))
                            continue;

                        ExceptionHandlingBehavior handlingBehavior = exceptionElement.Behavior;
                        if (exceptionElement.Handlers != null &&
                            exceptionElement.Handlers.Count > 0)
                        {
                            foreach (ExceptionHandlerElement exceptionHandlerElement in exceptionElement.Handlers)
                            {
                                Type handlerType = Type.GetType(exceptionHandlerElement.Type);
                                if (handlerType != null)
                                {
                                    if (handlerType.IsAbstract ||
                                        !handlerType.GetInterfaces().Any(p => p.Equals(typeof(IExceptionHandler))))
                                        continue;

                                    try
                                    {
                                        IExceptionHandler exceptionHandler = (IExceptionHandler)Activator.CreateInstance(handlerType);
                                        this.RegisterHandlerOrig(exceptionType, handlingBehavior, exceptionHandler);
                                    }
                                    catch
                                    {
                                        continue;
                                    } // try
                                } // if
                            } // foreach - exception handler
                        }
                        else
                        {
                            handlersOrig.Add(exceptionType, new ExceptionConfigItem { Behavior = handlingBehavior, Handlers = new List<IExceptionHandler>() });
                        }
                    } // foreach - exception
                    BuildResponsibilityChain();
                } // if
            }
            catch { }
        }
        #endregion

        #region 私有方法
        private void RegisterHandlerOrig(Type exceptionType, ExceptionHandlingBehavior behavior, IExceptionHandler handler)
        {
            if (handlersOrig.ContainsKey(exceptionType))
            {
                var exceptionConfigItem = handlersOrig[exceptionType];
                var list = exceptionConfigItem.Handlers;
                if (!list.Contains(handler, new ExceptionHandlerComparer()))
                {
                    list.Add(handler);
                }
            }
            else
            {
                ExceptionConfigItem configItem = new ExceptionConfigItem();
                configItem.Behavior = behavior;
                configItem.Handlers.Add(handler);
                handlersOrig[exceptionType] = configItem;
            }
        }

        private List<IExceptionHandler> DumpBaseHandlers(Type thisType)
        {
            List<IExceptionHandler> handlers = new List<IExceptionHandler>();
            Type baseType = thisType.BaseType;
            while (baseType != typeof(object))
            {
                if (handlersOrig.ContainsKey(baseType))
                {
                    var item = handlersOrig[baseType];
                    item.Handlers.ForEach(p => handlers.Add(p));
                    //break;
                }
                baseType = baseType.BaseType;
            }
            return handlers;
        }

        private void BuildResponsibilityChain()
        {
            foreach (var kvp in handlersOrig)
            {
                List<IExceptionHandler> handlers = new List<IExceptionHandler>();
                kvp.Value.Handlers.ForEach(p => handlers.Add(p));
                switch (kvp.Value.Behavior)
                {
                    case ExceptionHandlingBehavior.Direct:
                        break;
                    case ExceptionHandlingBehavior.Forward:
                        List<IExceptionHandler> handlersFromBase = DumpBaseHandlers(kvp.Key);
                        handlersFromBase.ForEach(p => handlers.Add(p));
                        break;
                    default:
                        break;
                }
                handlersResponsibilityChain.Add(kvp.Key, handlers);
            }
        }

        private bool HandleExceptionInternal(Exception ex)
        {
            Type exceptionType = ex.GetType();
            Type curType = exceptionType;
            while (curType != null && curType.IsClass && typeof(Exception).IsAssignableFrom(curType))
            {
                if (handlersResponsibilityChain.ContainsKey(curType))
                {
                    var handlers = handlersResponsibilityChain[curType];
                    if (handlers != null && handlers.Count > 0)
                    {
                        bool ret = false;
                        handlers.ForEach(p => ret |= p.HandleException(ex));
                        return ret; // if true, the exception was handled by at least one handler. otherwise false.
                    }
                    else
                        return false; // the exception was not handled.
                }
                curType = curType.BaseType;
            }
            return false; // no handler would handle the exception.
        }

        private IEnumerable<IExceptionHandler> GetExceptionHandlersInternal(Type exceptionType)
        {
            Type curType = exceptionType;
            while (curType != null && curType.IsClass && typeof(Exception).IsAssignableFrom(curType))
            {
                if (handlersResponsibilityChain.ContainsKey(curType))
                    return handlersResponsibilityChain[curType];
                curType = curType.BaseType;
            }
            return new List<IExceptionHandler>();
        }
        
        private IEnumerable<IExceptionHandler> GetExceptionHandlersInternal<TException>()
            where TException : Exception
        {
            return GetExceptionHandlers(typeof(TException));
        }
        
        private IEnumerable<Type> GetRegisteredExceptionTypesInternal()
        {
            return handlersResponsibilityChain.Keys;
        }
        #endregion

        #region 私有属性
        private static ExceptionManager InstanceInternal
        {
            get { return instance; }
        }
        private int RegisteredExceptionCountInternal
        {
            get { return handlersResponsibilityChain.Count; }
        }

        #endregion

        #region 公共属性
        /// <summary>
        /// 获取<see cref ="System.Int32"/>值，该值表示异常管理器中注册的异常数量.
        /// </summary>
        public static int RegisteredExceptionCount
        {
            get { return InstanceInternal.RegisteredExceptionCountInternal; }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取特定异常类型的异常处理程序列表.
        /// </summary>
        /// <param name="exceptionType">异常类型.</param>
        /// <returns>异常处理程序的列表.</returns>
        public static IEnumerable<IExceptionHandler> GetExceptionHandlers(Type exceptionType)
        {
            return InstanceInternal.GetExceptionHandlersInternal(exceptionType);
        }
        /// <summary>
        /// 获取特定异常类型的异常处理程序列表.
        /// </summary>
        /// <typeparam name="TException">异常类型.</typeparam>
        /// <returns>异常处理程序的列表.</returns>
        public static IEnumerable<IExceptionHandler> GetExceptionHandlers<TException>()
            where TException : Exception
        {
            return InstanceInternal.GetExceptionHandlersInternal<TException>();
        }
        /// <summary>
        /// 获取OFoods配置部分中注册的所有异常类型.
        /// </summary>
        /// <returns>已注册的异常类型列表.</returns>
        public static IEnumerable<Type> GetRegisteredExceptionTypes()
        {
            return InstanceInternal.GetRegisteredExceptionTypesInternal();
        }
        /// <summary>
        /// 处理特定的异常.
        /// </summary>
        /// <param name="ex">要处理的例外.</param>
        /// <returns>如果可以成功处理异常，则为true，否则为false.</returns>
        public static bool HandleException(Exception ex)
        {
            return InstanceInternal.HandleExceptionInternal(ex);
        }
        /// <summary>
        /// 处理特定的异常.
        /// </summary>
        /// <typeparam name="TException">要处理的异常的类型.</typeparam>
        /// <param name="ex">要处理的例外.</param>
        /// <returns>如果可以成功处理异常，则为true，否则为false.</returns>
        public static bool HandleException<TException>(TException ex)
            where TException : Exception
        {
            return InstanceInternal.HandleExceptionInternal((Exception)ex);
        }
        #endregion

        #region 内部类

        class ExceptionHandlerComparer : IEqualityComparer<IExceptionHandler>
        {
            public bool Equals(IExceptionHandler x, IExceptionHandler y)
            {
                return x.GetType().AssemblyQualifiedName.Equals(y.GetType().AssemblyQualifiedName);
            }

            public int GetHashCode(IExceptionHandler obj)
            {
                return obj.GetHashCode();
            }
        }
        /// <summary>
        /// 异常配置项
        /// </summary>
        class ExceptionConfigItem
        {
            public ExceptionHandlingBehavior Behavior { get; set; }
            public List<IExceptionHandler> Handlers { get; set; }

            public ExceptionConfigItem()
            {
                Behavior = ExceptionHandlingBehavior.Direct;
                Handlers = new List<IExceptionHandler>();
            }
        }
        #endregion
    }
}
