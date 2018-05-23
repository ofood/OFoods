using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;

namespace OFoods.Configurations.Elements
{
    /// <summary>
    /// 表示顺序生成器的配置.
    /// </summary>
    public partial class SequenceGeneratorElement : ConfigurationElement
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
        /// 获取或设置序列生成器的类型.
        /// </summary>
        [Description("序列生成器的类型.")]
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
    }
}
