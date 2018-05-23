using System;
namespace OFoods.Configurations.Fluent
{
    // <summary>
    /// 表示身份生成器配置程序.
    /// </summary>
    public class IdentityGeneratorConfigurator : TypeSpecifiedConfigSourceConfigurator, IIdentityGeneratorConfigurator
    {
        #region 构造函数
        /// <summary>
        /// 初始化<c> IdentityGeneratorConfigurator </ c>类的新实例.
        /// </summary>
        /// <param name="context">配置上下文.</param>
        /// <param name="identityGeneratorType">要在应用程序中使用的生成器类型.</param>
        public IdentityGeneratorConfigurator(IConfigSourceConfigurator context, Type identityGeneratorType)
            : base(context, identityGeneratorType)
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
            container.IdentityGenerator = Type;
            return container;
        }
        #endregion
    }
}
