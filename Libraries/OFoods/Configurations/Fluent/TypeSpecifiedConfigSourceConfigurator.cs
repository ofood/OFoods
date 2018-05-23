using System;

namespace OFoods.Configurations.Fluent
{
    /// <summary>
    /// 表示配置容器特定类型的配置配置器的基类.
    /// </summary>
    public abstract class TypeSpecifiedConfigSourceConfigurator : ConfigSourceConfigurator
    {
        #region 私有字段
        private readonly Type type;
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化<c> TypeSpecifiedConfigSourceConfigurator </ c>类的新实例.
        /// </summary>
        /// <param name="context">配置上下文.</param>
        /// <param name="type">配置所需的类型.</param>
        public TypeSpecifiedConfigSourceConfigurator(IConfigSourceConfigurator context, Type type)
            : base(context)
        {
            this.type = type;
        }
        #endregion

        #region 受保护属性
        /// <summary>
        /// 获取配置所需的类型.
        /// </summary>
        protected Type Type
        {
            get { return type; }
        }
        #endregion
    }
}
