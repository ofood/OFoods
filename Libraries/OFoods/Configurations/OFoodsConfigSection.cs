using OFoods.Configurations.Elements;
using OFoods.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace OFoods.Configurations
{
    public class OFoodsConfigSection: ConfigurationSection
    {
        #region 私有方法

        private InterceptContractElement FindInterceptContractElement(Type contractType)
        {
            if (this.Interception.Contracts == null)
                return null;

            foreach (InterceptContractElement interceptContractElement in this.Interception.Contracts)
            {
                if (interceptContractElement.Type.Equals(contractType.AssemblyQualifiedName))
                    return interceptContractElement;
            }
            return null;
        }

        private InterceptMethodElement FindInterceptMethodElement(InterceptContractElement interceptContractElement, MethodInfo method)
        {
            if (interceptContractElement == null)
                return null;
            if (interceptContractElement.Methods == null)
                return null;

            foreach (InterceptMethodElement interceptMethodElement in interceptContractElement.Methods)
            {
                string methodSignature = null;
                if (method.IsGenericMethod)
                    methodSignature = method.GetGenericMethodDefinition().GetSignature();
                else
                    methodSignature = method.GetSignature();
                if (interceptMethodElement.Signature.Equals(methodSignature))
                    return interceptMethodElement;
            }
            return null;
        }

        private IEnumerable<string> FindInterceptorRefNames(InterceptMethodElement interceptMethodElement)
        {
            if (interceptMethodElement == null)
                return null;
            if (interceptMethodElement.InterceptorRefs == null)
                return null;
            List<string> ret = new List<string>();
            foreach (InterceptorRefElement interceptorRefElement in interceptMethodElement.InterceptorRefs)
            {
                ret.Add(interceptorRefElement.Name);
            }
            return ret;
        }

        private string FindInterceptorTypeNameByRefName(string refName)
        {
            if (this.Interception == null || this.Interception.Interceptors == null)
                return null;
            foreach (InterceptorElement interceptorElement in this.Interception.Interceptors)
            {
                if (interceptorElement.Name.Equals(refName))
                    return interceptorElement.Type;
            }
            return null;
        }
        #endregion

        #region 公开方法
        /// <summary>
        /// 返回当前OFoods配置部分的序列化XML字符串.
        /// </summary>
        /// <returns>序列化的XML字符串.</returns>
        public string GetSerializedXmlString()
        {
            return this.SerializeSection(null, AppConfigSource.DefaultConfigSection, System.Configuration.ConfigurationSaveMode.Full);
        }

        /// <summary>
        /// 返回一个<see cref ="System.String"/>值的列表，它表示通过给定方法引用的拦截器引用的类型.
        /// </summary>
        /// <param name="contractType">该方法的类型.</param>
        /// <param name="method">方法.</param>
        /// <returns>包含拦截器类型的<see cref ="System.String"/>值列表.</returns>
        public IEnumerable<string> GetInterceptorTypes(Type contractType, MethodInfo method)
        {
            if (this.Interception == null ||
                this.Interception.Interceptors == null ||
                this.Interception.Contracts == null)
                return null;

            InterceptContractElement interceptContractElement = this.FindInterceptContractElement(contractType);
            if (interceptContractElement == null)
                return null;

            InterceptMethodElement interceptMethodElement = this.FindInterceptMethodElement(interceptContractElement, method);
            if (interceptMethodElement == null)
                return null;

            var interceptorRefNames = this.FindInterceptorRefNames(interceptMethodElement);
            if (interceptorRefNames == null || interceptorRefNames.Count() == 0)
                return null;

            List<string> ret = new List<string>();
            foreach (var interceptorRefName in interceptorRefNames)
            {
                var interceptorTypeName = this.FindInterceptorTypeNameByRefName(interceptorRefName);
                if (!string.IsNullOrEmpty(interceptorTypeName))
                    ret.Add(interceptorTypeName);
            }
            return ret;
        }
        #endregion

        #region Xmlns 属性
        /// <summary>
        /// <see cref ="Xmlns"/>属性的XML名称.
        /// </summary>
        internal const string XmlnsPropertyName = "xmlns";

        /// <summary>
        /// 获取此配置节的XML名称空间.
        /// </summary>
        /// <remarks>
        /// 该属性确保如果配置文件包含XML名称空间，则解析器不会抛出异常，因为它遇到未知的“xmlns”属性.
        /// </remarks>
        [ConfigurationProperty(XmlnsPropertyName, IsRequired = false, IsKey = false, IsDefaultCollection = false)]
        public string Xmlns
        {
            get
            {
                return ((string)(base[XmlnsPropertyName]));
            }
        }
        #endregion

        #region IsReadOnly override
        /// <summary>
        /// 获取一个值，指示元素是否是只读的.
        /// </summary>
        public override bool IsReadOnly()
        {
            return false;
        }
        #endregion

        #region Application 属性
        /// <summary>
        /// The XML name of the <see cref="Application"/> property.
        /// </summary>
        internal const string ApplicationPropertyName = "application";

        /// <summary>
        /// 获取或设置OFoods应用程序的配置.
        /// </summary>
        [Description("OFoods应用程序的配置.")]
        [ConfigurationProperty(ApplicationPropertyName, IsRequired = true, IsKey = false, IsDefaultCollection = false)]
        public virtual ApplicationElement Application
        {
            get
            {
                return ((ApplicationElement)(base[ApplicationPropertyName]));
            }
            set
            {
                base[ApplicationPropertyName] = value;
            }
        }
        #endregion

        #region ObjectContainer 属性
        /// <summary>
        /// The XML name of the <see cref="ObjectContainer"/> property.
        /// </summary>
        internal const string ObjectContainerPropertyName = "objectContainer";

        /// <summary>
        /// 获取或设置对象容器的配置.
        /// </summary>
        [Description("对象容器的配置.")]
        [ConfigurationProperty(ObjectContainerPropertyName, IsRequired = true, IsKey = false, IsDefaultCollection = false)]
        public virtual ObjectContainerElement ObjectContainer
        {
            get
            {
                return ((ObjectContainerElement)(base[ObjectContainerPropertyName]));
            }
            set
            {
                base[ObjectContainerPropertyName] = value;
            }
        }
        #endregion

        #region Serializers 属性
        /// <summary>
        /// The XML name of the <see cref="Serializers"/> property.
        /// </summary>
        internal const string SerializersPropertyName = "serializers";

        /// <summary>
        /// 获取或设置序列化的配置.
        /// </summary>
        [Description("序列化的配置.")]
        [ConfigurationProperty(SerializersPropertyName, IsRequired = false, IsKey = false, IsDefaultCollection = false)]
        public virtual SerializerElement Serializers
        {
            get
            {
                return ((SerializerElement)(base[SerializersPropertyName]));
            }
            set
            {
                base[SerializersPropertyName] = value;
            }
        }
        #endregion

        #region Generators 属性
        /// <summary>
        /// <see cref ="Generators"/>属性的XML名称.
        /// </summary>
        internal const string GeneratorsPropertyName = "generators";

        /// <summary>
        /// 获取或设置标识和顺序生成器的配置.
        /// </summary>
        [Description("标识和顺序生成器的配置.")]
        [ConfigurationProperty(GeneratorsPropertyName, IsRequired = false, IsKey = false, IsDefaultCollection = false)]
        public virtual GeneratorElement Generators
        {
            get
            {
                return ((GeneratorElement)(base[GeneratorsPropertyName]));
            }
            set
            {
                base[GeneratorsPropertyName] = value;
            }
        }
        #endregion

        #region Handlers 属性
        /// <summary>
        /// <see cref ="Handlers"/>属性的XML名称.
        /// </summary>
        internal const string HandlersPropertyName = "handlers";

        /// <summary>
        /// 获取或设置命令或事件处理程序的配置.
        /// </summary>
        [Description("命令或事件处理程序的配置.")]
        [ConfigurationProperty(HandlersPropertyName, IsRequired = false, IsKey = false, IsDefaultCollection = false)]
        public virtual HandlerElementCollection Handlers
        {
            get
            {
                return ((HandlerElementCollection)(base[HandlersPropertyName]));
            }
            set
            {
                base[HandlersPropertyName] = value;
            }
        }
        #endregion

        #region Exceptions 属性
        /// <summary>
        /// The XML name of the <see cref="Exceptions"/> property.
        /// </summary>
        internal const string ExceptionsPropertyName = "exceptions";

        /// <summary>
        /// 获取或设置OFoods框架中异常处理逻辑的配置.
        /// </summary>
        [Description("OFoods框架内异常处理逻辑的配置.")]
        [ConfigurationProperty(ExceptionsPropertyName, IsRequired = false, IsKey = false, IsDefaultCollection = false)]
        public virtual ExceptionElementCollection Exceptions
        {
            get
            {
                return ((ExceptionElementCollection)(base[ExceptionsPropertyName]));
            }
            set
            {
                base[ExceptionsPropertyName] = value;
            }
        }
        #endregion

        #region Interception 属性
        /// <summary>
        /// The XML name of the <see cref="Interception"/> property.
        /// </summary>
        internal const string InterceptionPropertyName = "interception";

        /// <summary>
        /// 获取或设置Aop拦截的配置.
        /// </summary>
        [Description("The configuration for the interceptions.")]
        [ConfigurationProperty(InterceptionPropertyName, IsRequired = false, IsKey = false, IsDefaultCollection = false)]
        public virtual InterceptionElement Interception
        {
            get
            {
                return ((InterceptionElement)(base[InterceptionPropertyName]));
            }
            set
            {
                base[InterceptionPropertyName] = value;
            }
        }
        #endregion
    }
}
