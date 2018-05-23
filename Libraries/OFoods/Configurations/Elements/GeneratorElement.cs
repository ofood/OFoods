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
    /// Generator元素包含顺序或标识生成器的配置.
    /// </summary>
    public partial class GeneratorElement :ConfigurationElement
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

        #region SequenceGenerator Property
        /// <summary>
        /// <see cref ="SequenceGenerator"/>属性的XML名称.
        /// </summary>
        internal const string SequenceGeneratorPropertyName = "sequenceGenerator";

        /// <summary>
        /// 获取或设置序列生成器的配置.
        /// </summary>
        [Description("序列生成器的配置.")]
        [ConfigurationProperty(SequenceGeneratorPropertyName, IsRequired = true, IsKey = false, IsDefaultCollection = false)]
        public virtual SequenceGeneratorElement SequenceGenerator
        {
            get
            {
                return ((SequenceGeneratorElement)(base[SequenceGeneratorPropertyName]));
            }
            set
            {
                base[SequenceGeneratorPropertyName] = value;
            }
        }
        #endregion

        #region IdentityGenerator Property
        /// <summary>
        /// <see cref ="IdentityGenerator"/>属性的XML名称.
        /// </summary>
        internal const string IdentityGeneratorPropertyName = "identityGenerator";

        /// <summary>
        /// 获取或设置标识生成器的配置.
        /// </summary>
        [Description("标识生成器的配置.")]
        [ConfigurationProperty(IdentityGeneratorPropertyName, IsRequired = true, IsKey = false, IsDefaultCollection = false)]
        public virtual IdentityGeneratorElement IdentityGenerator
        {
            get
            {
                return ((IdentityGeneratorElement)(base[IdentityGeneratorPropertyName]));
            }
            set
            {
                base[IdentityGeneratorPropertyName] = value;
            }
        }
        #endregion
    }
}
