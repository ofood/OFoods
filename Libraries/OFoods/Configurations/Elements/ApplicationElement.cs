using System.Configuration;

namespace OFoods.Configurations.Elements
{
    /// <summary>
    /// 应用程序元素
    /// </summary>
    public partial class ApplicationElement : ConfigurationElement
    {

        /// <summary>
        /// 获取一个值，指示该元素是否是只读的.
        /// </summary>
        public override bool IsReadOnly()
        {
            return false;
        }

        #region Provider 属性
        /// <summary>
        /// <see cref ="Provider"/>属性的XML名称.
        /// </summary>
        internal const string ProviderPropertyName = "provider";

        /// <summary>
        /// 获取或设置应用程序的提供程序类型.
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("The provider type of the application.")]
        [ConfigurationPropertyAttribute(ProviderPropertyName, IsRequired = true, IsKey = true, IsDefaultCollection = false)]
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
