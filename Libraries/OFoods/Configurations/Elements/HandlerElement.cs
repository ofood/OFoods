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
    /// 表示消息处理程序的配置.
    /// </summary>
    public partial class HandlerElement : ConfigurationElement
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

        #region Kind Property
        /// <summary>
        /// <see cref ="Kind"/>属性的XML名称.
        /// </summary>
        internal const string KindPropertyName = "kind";

        /// <summary>
        /// 获取或设置处理程序的类型，可以是Command或Event.
        /// </summary>
        [Description("处理程序的类型可以是Command或Event.")]
        [ConfigurationProperty(KindPropertyName, IsRequired = false, IsKey = false, IsDefaultCollection = false)]
        public virtual HandlerKind Kind
        {
            get
            {
                return ((HandlerKind)(base[KindPropertyName]));
            }
            set
            {
                base[KindPropertyName] = value;
            }
        }
        #endregion

        #region Name Property
        /// <summary>
        /// <see cref ="Name"/>属性的XML名称.
        /// </summary>
        internal const string NamePropertyName = "name";

        /// <summary>
        /// 获取或设置处理程序的名称.
        /// </summary>
        [Description("处理程序的名称.")]
        [ConfigurationProperty(NamePropertyName, IsRequired = true, IsKey = true, IsDefaultCollection = false)]
        public virtual string Name
        {
            get
            {
                return ((string)(base[NamePropertyName]));
            }
            set
            {
                base[NamePropertyName] = value;
            }
        }
        #endregion

        #region SourceType Property
        /// <summary>
        /// <see cref ="SourceType"/>属性的XML名称.
        /// </summary>
        internal const string SourceTypePropertyName = "sourceType";

        /// <summary>
        /// 获取或设置源类型，可以是Assembly或Type.
        /// </summary>
        [Description("源类型可以是Assembly或Type.")]
        [ConfigurationProperty(SourceTypePropertyName, IsRequired = true, IsKey = false, IsDefaultCollection = false)]
        public virtual HandlerSourceType SourceType
        {
            get
            {
                return ((HandlerSourceType)(base[SourceTypePropertyName]));
            }
            set
            {
                base[SourceTypePropertyName] = value;
            }
        }
        #endregion

        #region Source Property
        /// <summary>
        /// <see cref ="Source"/>属性的XML名称.
        /// </summary>
        internal const string SourcePropertyName = "source";

        /// <summary>
        /// 获取或设置源的名称，当SourceType为Assembly时，该名称可以是程序集名称，也可以是源类型为Type时的类型名称.
        /// </summary>
        [Description("当SourceType为“+”时，源的名称可以是程序集名称“是Assembly，或者是类型名称，当SourceType是Type时.")]
        [ConfigurationProperty(SourcePropertyName, IsRequired = true, IsKey = false, IsDefaultCollection = false)]
        public virtual string Source
        {
            get
            {
                return ((string)(base[SourcePropertyName]));
            }
            set
            {
                base[SourcePropertyName] = value;
            }
        }
        #endregion
    }
}
