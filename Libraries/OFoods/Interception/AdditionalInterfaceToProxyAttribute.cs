using System;

namespace OFoods.Interception
{
    /// <summary>
    /// 表示当Castle Dynamic Proxy为这些类创建代理对象时，修饰类需要额外的接口被拦截.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true, Inherited=false)]
    public class AdditionalInterfaceToProxyAttribute : System.Attribute
    {
        /// <summary>
        /// 获取或设置创建代理对象时需要拦截的接口的类型.
        /// </summary>
        public Type InterfaceType { get; set; }

        /// <summary>
        /// 初始化<c> AdditionalInterfaceToProxyAttribute </ c>的新实例.
        /// </summary>
        /// <param name="intfType">代理对象创建时需要拦截的接口的类型.</param>
        public AdditionalInterfaceToProxyAttribute(Type intfType)
        {
            this.InterfaceType = intfType;
        }
    }
}
