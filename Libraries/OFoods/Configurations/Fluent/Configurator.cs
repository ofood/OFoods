using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoods.Configurations.Fluent
{
    /// <summary>
    /// 表示所有配置配置器的基类.
    /// </summary>
    /// <typeparam name="TContainer">对象容器的类型.</typeparam>
    public abstract class Configurator<TContainer>:IConfigurator<TContainer>
    {
        private readonly IConfigurator<TContainer> context;
        /// <summary>
        /// 初始化<c>配置器{TContainer} </ c>类的新实例.
        /// </summary>
        /// <param name="context"><see cref ="IConfigurator {TContainer}"/>实例.</param>
        /// <remarks>The <paramref name="context"/> 参数指定由上一个配置步骤提供并将在当前步骤中配置的配置上下文.</remarks>
        public Configurator(IConfigurator<TContainer> context)
        {
            this.context = context;
        }
        /// <summary>
        /// 获取配置上下文实例.
        /// </summary>
        public IConfigurator<TContainer> Context
        {
            get { return this.context; }
        }

        /// <summary>
        /// 配置容器.
        /// </summary>
        /// <param name="container">要配置的对象容器.</param>
        /// <returns>已配置的容器.</returns>
        protected abstract TContainer DoConfigure(TContainer container);

        /// <summary>
        /// 配置容器.
        /// </summary>
        /// <returns>已配置的容器.</returns>
        public TContainer Configure()
        {
            var container = this.context.Configure();
            return DoConfigure(container);
        }
    }
}
