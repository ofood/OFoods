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
    /// 表示侦听契约的配置
    /// </summary>
    public class InterceptContractElement: ConfigurationElement
    {
        public override bool IsReadOnly()
        {
            return false;
        }
        /// <summary>
        /// <see cref =“Type”/>属性的XML名称.
        /// </summary>
        internal const string TypePropertyName = "type";
        /// <summary>
        /// 获取或设置类型.
        /// </summary>
        [Description("The Type.")]
        [ConfigurationProperty(TypePropertyName, IsRequired = true, IsKey = true, IsDefaultCollection = false)]
        public virtual string Type
        {
            get
            {
                return ((string)(base[TypePropertyName]));
            }
            set
            {
                base[TypePropertyName] = value;
            }
        }

        #region Methods Property
        /// <summary>
        /// The XML name of the <see cref="Methods"/> property.
        /// </summary>
        internal const string MethodsPropertyName = "methods";

        /// <summary>
        /// Gets or sets the Methods.
        /// </summary>
        [Description("The Methods.")]
        [ConfigurationProperty(MethodsPropertyName, IsRequired = false, IsKey = false, IsDefaultCollection = false)]
        public virtual InterceptMethodElementCollection Methods
        {
            get
            {
                return ((InterceptMethodElementCollection)(base[MethodsPropertyName]));
            }
            set
            {
                base[MethodsPropertyName] = value;
            }
        }
        #endregion
    }
}
