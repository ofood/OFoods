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
    /// 表示快照序列化程序的配置.
    /// </summary>
    public partial class SnapshotSerializerElement : ConfigurationElement
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

        #region 提供者属性
        /// <summary>
        /// <see cref ="Provider"/>属性的XML名称.
        /// </summary>
        internal const string ProviderPropertyName = "provider";

        /// <summary>
        /// 获取或设置快照序列化程序的提供程序类型.
        /// </summary>
        [Description("快照序列化程序的提供程序类型.")]
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
