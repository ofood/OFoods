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
    /// 表示拦截器引用的配置.
    /// </summary>
    public class InterceptorRefElement : ConfigurationElement
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
    }
}
