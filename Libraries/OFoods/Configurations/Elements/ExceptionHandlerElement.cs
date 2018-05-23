
using System.Configuration;
using System.ComponentModel;
namespace OFoods.Configurations.Elements
{
    /// <summary>
    /// 表示异常处理程序的配置.
    /// </summary>
    public partial class ExceptionHandlerElement : ConfigurationElement
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

        #region Type Property
        /// <summary>
        /// <see cref =“Type”/>属性的XML名称.
        /// </summary>
        internal const string TypePropertyName = "type";

        /// <summary>
        /// 获取或设置异常处理程序的类型.
        /// </summary>
        [Description("异常处理程序的类型.")]
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
        #endregion
    }
}
