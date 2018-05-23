using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace OFoods.Configurations.Elements
{
    /// <summary>
    /// 代表标识生成器的配置.
    /// </summary>
    public partial class IdentityGeneratorElement :ConfigurationElement
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
        /// Gets or sets the type of identity generator.
        /// </summary>
        [Description("标识生成器的类型.")]
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
