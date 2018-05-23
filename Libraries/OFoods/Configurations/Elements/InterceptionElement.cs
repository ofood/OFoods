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
    /// 表示截取的配置.
    /// </summary>
    public partial class InterceptionElement : ConfigurationElement
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

        #region Interceptors Property
        /// <summary>
        /// <see cref ="Interceptors"/>属性的XML名称.
        /// </summary>
        internal const string InterceptorsPropertyName = "interceptors";

        /// <summary>
        /// 获取或设置拦截器.
        /// </summary>
        [Description("The Interceptors.")]
        [ConfigurationProperty(InterceptorsPropertyName, IsRequired = false, IsKey = false, IsDefaultCollection = false)]
        public virtual InterceptorElementCollection Interceptors
        {
            get
            {
                return ((InterceptorElementCollection)(base[InterceptorsPropertyName]));
            }
            set
            {
                base[InterceptorsPropertyName] = value;
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
        [ConfigurationProperty(ContractsPropertyName, IsRequired = false, IsKey = false, IsDefaultCollection = false)]
        public virtual InterceptContractElementCollection Contracts
        {
            get
            {
                return ((InterceptContractElementCollection)(base[ContractsPropertyName]));
            }
            set
            {
                base[ContractsPropertyName] = value;
            }
        }
        #endregion
    }
}
