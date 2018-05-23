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
    /// 表示异常处理的配置.
    /// </summary>
    public partial class ExceptionElement : ConfigurationElement
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
        /// 获取或设置异常的类型.
        /// </summary>
        [Description("例外的类型.")]
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

        #region Behavior Property
        /// <summary>
        /// <see cref =“Behavior”/>属性的XML名称.
        /// </summary>
        internal const string BehaviorPropertyName = "behavior";

        /// <summary>
        /// 获取或设置异常处理的行为.
        /// </summary>
        [Description("异常处理的行为.")]
        [ConfigurationProperty(BehaviorPropertyName, IsRequired = false, IsKey = false, IsDefaultCollection = false)]
        public virtual ExceptionHandlingBehavior Behavior
        {
            get
            {
                return ((ExceptionHandlingBehavior)(base[BehaviorPropertyName]));
            }
            set
            {
                base[BehaviorPropertyName] = value;
            }
        }
        #endregion

        #region Handlers Property
        /// <summary>
        /// <see cref =“Handlers”/>属性的XML名称.
        /// </summary>
        internal const string HandlersPropertyName = "handlers";

        /// <summary>
        /// 获取或设置处理程序.
        /// </summary>
        [Description("The Handlers.")]
        [ConfigurationProperty(HandlersPropertyName, IsRequired = false, IsKey = false, IsDefaultCollection = false)]
        public virtual ExceptionHandlerElementCollection Handlers
        {
            get
            {
                return ((ExceptionHandlerElementCollection)(base[HandlersPropertyName]));
            }
            set
            {
                base[HandlersPropertyName] = value;
            }
        }
        #endregion
    }
}
