using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoods.Configurations.Fluent
{
    /// <summary>
    /// 表示对象容器配置器.
    /// </summary>
    public class ObjectContainerConfigurator : TypeSpecifiedConfigSourceConfigurator, IObjectContainerConfigurator
    {
        private readonly bool initFromConfigFile = false;
        private readonly string sectionName = null;
        #region Ctor
        /// <summary>
        /// 初始化<c> ObjectContainerConfigurator </ c>类的新实例.
        /// </summary>
        /// <param name="context">配置上下文.</param>
        /// <param name="objectContainerType">要由应用程序使用的对象容器的类型.</param>
        /// <param name="initFromConfigFile"><see cref ="Boolean"/>值指示是否应该从配置文件读取容器配置.</param>
        /// <param name="sectionName">配置文件中的部分名称。 当<paramref name ="initFromConfigFile"/>参数设置为true时，必须指定此值.</param>
        public ObjectContainerConfigurator(IConfigSourceConfigurator context, Type objectContainerType, bool initFromConfigFile, string sectionName)
            : base(context, objectContainerType)
        {
            this.initFromConfigFile = initFromConfigFile;
            this.sectionName = sectionName;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// 配置容器.
        /// </summary>
        /// <param name="container">配置容器.</param>
        /// <returns>已配置的容器.</returns>
        protected override RegularConfigSource DoConfigure(RegularConfigSource container)
        {
            container.ObjectContainer = Type;
            container.InitObjectContainerFromConfigFile = this.initFromConfigFile;
            container.ObjectContainerSectionName = this.sectionName;
            return container;
        }
        #endregion
    }
}
