using System;
namespace OFoods.Configurations.Fluent
{
    /// <summary>
    /// 代表应用程序配置器.
    /// </summary>
    public class ApplicationConfigurator: TypeSpecifiedConfigSourceConfigurator, IApplicationConfigurator
    {
        #region 构造函数
        /// <summary>
        /// 初始化<c> ApplicationConfigurator</c>类的新实例.
        /// </summary>
        /// <param name="context">配置上下文.</param>
        /// <param name="appType">应用程序的类型.</param>
        public ApplicationConfigurator(IConfigSourceConfigurator context, Type appType)
            : base(context, appType)
        { }
        #endregion

        #region 受保护方法
        /// <summary>
        /// 配置容器.
        /// </summary>
        /// <param name="container">配置容器.</param>
        /// <returns>已配置容器.</returns>
        protected override RegularConfigSource DoConfigure(RegularConfigSource container)
        {
            container.Application = Type;
            return container;
        }
        #endregion  
    }
}
