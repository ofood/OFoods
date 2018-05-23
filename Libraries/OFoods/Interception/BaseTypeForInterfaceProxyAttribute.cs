using System;

namespace OFoods.Interception
{
    /// <summary>
    /// 表示装饰类在其接口由Castle动态代理机制代理时需要基本类型.
    /// </summary>
    /// <remarks>通过在类上使用该属性，
    /// 当Castle Dynamic Proxy框架为该类的接口创建代理类时，
    /// 此属性中指定的基类型将用作创建的代理类的基类型.</remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=false)]
    public class BaseTypeForInterfaceProxyAttribute : Attribute
    {
        /// <summary>
        /// 获取或设置生成的代理对象应从中派生的基类型.
        /// </summary>
        public Type BaseType { get; set; }

        /// <summary>
        /// 初始化<c>BaseTypeForInterfaceProxyAttribute</c>类的新实例.
        /// </summary>
        /// <param name="baseType">生成的代理对象应从中派生的基本类型.</param>
        public BaseTypeForInterfaceProxyAttribute(Type baseType)
        {
            this.BaseType = baseType;
        }
    }
}
