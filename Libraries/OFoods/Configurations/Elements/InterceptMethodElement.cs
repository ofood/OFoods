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
    /// 表示拦截方法的配置.
    /// </summary>
    public class InterceptMethodElement : ConfigurationElement
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

        #region Signature Property
        /// <summary>
        /// <see cref =“Signature”/>属性的XML名称.
        /// </summary>
        internal const string SignaturePropertyName = "signature";

        /// <summary>
        /// 获取或设置签名.
        /// </summary>
        [Description("The Signature.")]
        [ConfigurationProperty(SignaturePropertyName, IsRequired = true, IsKey = true, IsDefaultCollection = false)]
        public virtual string Signature
        {
            get
            {
                return ((string)(base[SignaturePropertyName]));
            }
            set
            {
                base[SignaturePropertyName] = value;
            }
        }
        #endregion

        #region InterceptorRefs Property
        /// <summary>
        /// <see cref ="InterceptorRefs"/>属性的XML名称.
        /// </summary>
        internal const string InterceptorRefsPropertyName = "interceptorRefs";

        /// <summary>
        /// 获取或设置InterceptorRefs.
        /// </summary>
        [Description("The InterceptorRefs.")]
        [ConfigurationProperty(InterceptorRefsPropertyName, IsRequired = false, IsKey = false, IsDefaultCollection = false)]
        public virtual InterceptorRefElementCollection InterceptorRefs
        {
            get
            {
                return ((InterceptorRefElementCollection)(base[InterceptorRefsPropertyName]));
            }
            set
            {
                base[InterceptorRefsPropertyName] = value;
            }
        }
        #endregion
    }
}
