using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OFoods.Configurations.Fluent
{
    /// <summary>
    /// 代表拦截配置器.
    /// </summary>
    public class InterceptionConfigurator : ConfigSourceConfigurator, IInterceptionConfigurator
    {
        #region Private Fields
        private readonly Type interceptorType;
        private readonly Type contractType;
        private readonly MethodInfo interceptMethod;
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化<c> InterceptionConfigurator </ c>类的新实例.
        /// </summary>
        /// <param name="context">配置上下文.</param>
        /// <param name="interceptorType">要注册的拦截器的类型.</param>
        /// <param name="contractType">需要拦截的类型.</param>
        /// <param name="interceptMethod">需要拦截的<see cref ="MethodInfo"/>实例.</param>
        public InterceptionConfigurator(IConfigSourceConfigurator context, Type interceptorType, Type contractType, MethodInfo interceptMethod)
            : base(context)
        {
            this.interceptorType = interceptorType;
            this.contractType = contractType;
            this.interceptMethod = interceptMethod;
        }
        /// <summary>
        /// 初始化<c> InterceptionConfigurator </ c>类的新实例.
        /// </summary>
        /// <param name="context">配置上下文.</param>
        /// <param name="interceptorType">要注册的拦截器的类型.</param>
        /// <param name="contractType">需要拦截的类型.</param>
        /// <param name="interceptMethod">需要拦截的方法的名称.</param>
        public InterceptionConfigurator(IConfigSourceConfigurator context, Type interceptorType, Type contractType, string interceptMethod)
            : base(context)
        {
            this.interceptorType = interceptorType;
            this.contractType = contractType;
            var method = contractType.GetMethod(interceptMethod, BindingFlags.Public | BindingFlags.Instance);
            if (method != null)
                this.interceptMethod = method;
            else
                throw new ConfigException("The method {0} requested doesn't exist in type {1}.", interceptMethod, contractType);
        }
        #endregion

        #region 受保护方法
        /// <summary>
        /// 配置容器.
        /// </summary>
        /// <param name="container">配置容器.</param>
        /// <returns>已配置的容器.</returns>
        protected override RegularConfigSource DoConfigure(RegularConfigSource container)
        {
            var name = this.interceptorType.FullName;
            container.AddInterceptor(name, this.interceptorType);
            container.AddInterceptorRef(this.contractType, this.interceptMethod, name);
            return container;
        }
        #endregion
    }
}
