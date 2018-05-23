using OFoods.Configurations.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoods.Configurations.Fluent
{
    /// <summary>
    /// 表示异常处理程序配置程序.
    /// </summary>
    public class ExceptionHandlerConfigurator : ConfigSourceConfigurator, IExceptionHandlerConfigurator
    {
        private readonly Type exceptionType;
        private readonly Type exceptionHandlerType;
        private readonly ExceptionHandlingBehavior behavior;

        #region 构造函数
        /// <summary>
        /// 初始化<c> ExceptionHandlerConfigurator </ c>类的新实例.
        /// </summary>
        /// <param name="context">配置上下文.</param>
        /// <param name="exceptionType">要处理的异常的类型.</param>
        /// <param name="exceptionHandlerType">异常处理程序的类型.</param>
        /// <param name="behavior">异常处理行为.</param>
        public ExceptionHandlerConfigurator(IConfigSourceConfigurator context, Type exceptionType, Type exceptionHandlerType, ExceptionHandlingBehavior behavior)
            : base(context)
        {
            this.exceptionType = exceptionType;
            this.exceptionHandlerType = exceptionHandlerType;
            this.behavior = behavior;
        }
        /// <summary>
        /// 初始化<c> ExceptionHandlerConfigurator </ c>类的新实例.
        /// </summary>
        /// <param name="context">配置上下文.</param>
        /// <param name="exceptionType">要处理的异常的类型.</param>
        /// <param name="exceptionHandlerType">异常处理程序的类型.</param>
        public ExceptionHandlerConfigurator(IConfigSourceConfigurator context, Type exceptionType, Type exceptionHandlerType)
            : this(context, exceptionType, exceptionHandlerType, ExceptionHandlingBehavior.Direct)
        { }
        #endregion

        #region Protected Methods
        /// <summary>
        /// 配置容器.
        /// </summary>
        /// <param name="container">配置容器.</param>
        /// <returns>已配置容器.</returns>
        protected override RegularConfigSource DoConfigure(RegularConfigSource container)
        {
            container.AddException(this.exceptionType, behavior);
            container.AddExceptionHandler(this.exceptionType, this.exceptionHandlerType);
            return container;
        }
        #endregion
    }
}
