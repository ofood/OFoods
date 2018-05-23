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
    /// InterceptorElement配置元素.
    /// </summary>
    public partial class InterceptorElement : ConfigurationElement
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

        #region 名称属性
        /// <summary>
        /// <see cref ="Name"/>属性的XML名称.
        /// </summary>
        internal const string NamePropertyName = "name";

        /// <summary>
        /// 获取或设置名称.
        /// </summary>
        [Description("The Name.")]
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

        #region Type Property
        /// <summary>
        /// <see cref ="Type"/>属性的XML名称.
        /// </summary>
        internal const string TypePropertyName = "type";

        /// <summary>
        /// 获取或设置类型.
        /// </summary>
        [Description("The Type.")]
        [ConfigurationProperty(TypePropertyName, IsRequired = false, IsKey = false, IsDefaultCollection = false)]
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

        #region Interceptors Property
        /// <summary>
        /// <see cref ="Interceptors"/>属性的XML名称.
        /// </summary>
        internal const string InterceptorsPropertyName = "interceptors";

        /// <summary>
        /// 获取或设置拦截器.
        /// </summary>
        [Description("The Interceptors.")]
        [ConfigurationProperty(InterceptionElement.InterceptorsPropertyName, IsRequired = false, IsKey = false, IsDefaultCollection = false)]
        public virtual InterceptorElementCollection Interceptors
        {
            get
            {
                return ((InterceptorElementCollection)(base[InterceptionElement.InterceptorsPropertyName]));
            }
            set
            {
                base[InterceptionElement.InterceptorsPropertyName] = value;
            }
        }
        #endregion

        #region Contracts Property
        /// <summary>
        /// <see cref ="Contracts"/>属性的XML名称.
        /// </summary>
        internal const string ContractsPropertyName = "contracts";

        /// <summary>
        /// 获取或设置合同.
        /// </summary>
        [Description("The Contracts.")]
        [ConfigurationProperty(InterceptionElement.ContractsPropertyName, IsRequired = false, IsKey = false, IsDefaultCollection = false)]
        public virtual InterceptContractElementCollection Contracts
        {
            get
            {
                return ((InterceptContractElementCollection)(base[InterceptionElement.ContractsPropertyName]));
            }
            set
            {
                base[InterceptionElement.ContractsPropertyName] = value;
            }
        }
        #endregion
    }
}
