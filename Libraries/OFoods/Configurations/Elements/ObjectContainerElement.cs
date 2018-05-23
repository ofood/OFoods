using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;

namespace OFoods.Configurations.Elements
{
    /// <summary>
    /// 对象容器元素.
    /// </summary>
    public partial class ObjectContainerElement : ConfigurationElement
    {

        #region IsReadOnly override
        /// <summary>
        /// 获取一个值，指示元素是否是只读的.
        /// </summary>
        public override bool IsReadOnly()
        {
            return false;
        }
        #endregion

        #region Provider Property
        /// <summary>
        /// <see cref ="Provider"/>属性的XML名称.
        /// </summary>
        internal const string ProviderPropertyName = "provider";

        /// <summary>
        /// 获取或设置对象容器的提供者类型.
        /// </summary>
        [Description("对象容器的提供者类型.")]
        [ConfigurationProperty(ProviderPropertyName, IsRequired = true, IsKey = true, IsDefaultCollection = false)]
        public virtual string Provider
        {
            get
            {
                return ((string)(base[ProviderPropertyName]));
            }
            set
            {
                base[ProviderPropertyName] = value;
            }
        }
        #endregion

        #region InitFromConfigFile Property
        /// <summary>
        /// <see cref ="InitFromConfigFile"/>属性的XML名称.
        /// </summary>
        internal const string InitFromConfigFilePropertyName = "initFromConfigFile";

        /// <summary>
        /// 获取或设置布尔值，该值指示是否应从app / web.config文件初始化对象容器配置.
        /// </summary>
        [Description("指示对象容器配置是否为“+”的布尔值“ld将从app / web.config文件初始化.")]
        [ConfigurationProperty(InitFromConfigFilePropertyName, IsRequired = true, IsKey = false, IsDefaultCollection = false, DefaultValue = false)]
        public virtual bool InitFromConfigFile
        {
            get
            {
                return ((bool)(base[InitFromConfigFilePropertyName]));
            }
            set
            {
                base[InitFromConfigFilePropertyName] = value;
            }
        }
        #endregion

        #region SectionName Property
        /// <summary>
        /// <see cref ="SectionName"/>属性的XML名称.
        /// </summary>
        internal const string SectionNamePropertyName = "sectionName";

        /// <summary>
        /// 获取或设置对象容器使用的配置节的名称（如果它旨在从app / web.config文件初始化）.
        /// </summary>
        [Description("对象容器将使用的配置节的名称“+”“如果它旨在从app / web.config文件初始化.")]
        [ConfigurationProperty(SectionNamePropertyName, IsRequired = false, IsKey = false, IsDefaultCollection = false)]
        public virtual string SectionName
        {
            get
            {
                return ((string)(base[SectionNamePropertyName]));
            }
            set
            {
                base[SectionNamePropertyName] = value;
            }
        }
        #endregion
    }
}
