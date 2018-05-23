using OFoods.Configurations.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoods.Configurations.Fluent
{
    /// <summary>
    /// 表示消息处理程序配置程序.
    /// </summary>
    public class HandlerConfigurator : ConfigSourceConfigurator, IHandlerConfigurator
    {
        #region 私有字段
        private readonly string name;
        private readonly HandlerKind handlerKind;
        private readonly HandlerSourceType sourceType;
        private readonly string source;
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化<c> HandlerConfigurator </ c>类的新实例.
        /// </summary>
        /// <param name="context">配置上下文.</param>
        /// <param name="name">消息处理程序的名称.</param>
        /// <param name="handlerKind">指定处理程序类型的<see cref ="HandlerKind"/>可以是Command或Event.</param>
        /// <param name="sourceType">指定源的类型的<see cref ="HandlerSourceType"/>可以是程序集或类型.</param>
        /// <param name="source">The source name, if <paramref name="sourceType"/> is Assembly, the source name should be the assembly full name, if
        /// <paramref name="sourceType"/> is Type, the source name should be the assembly qualified name of the type.</param>
        public HandlerConfigurator(IConfigSourceConfigurator context, string name, HandlerKind handlerKind,
            HandlerSourceType sourceType, string source)
            : base(context)
        {
            this.name = name;
            this.handlerKind = handlerKind;
            this.sourceType = sourceType;
            this.source = source;
        }
        /// <summary>
        /// 初始化<c> HandlerConfigurator </ c>类的新实例.
        /// </summary>
        /// <param name="context">配置上下文.</param>
        /// <param name="handlerKind">The <see cref="HandlerKind"/> which specifies the kind of the handler, can either be a Command or an Event.</param>
        /// <param name="sourceType">The <see cref="HandlerSourceType"/> which specifies the type of the source, can either be an Assembly or a Type.</param>
        /// <param name="source">The source name, if <paramref name="sourceType"/> is Assembly, the source name should be the assembly full name, if
        /// <paramref name="sourceType"/> is Type, the source name should be the assembly qualified name of the type.</param>
        public HandlerConfigurator(IConfigSourceConfigurator context, HandlerKind handlerKind,
            HandlerSourceType sourceType, string source)
            : this(context, Guid.NewGuid().ToString(), handlerKind, sourceType, source) { }
        #endregion

        #region 受保护方法
        /// <summary>
        /// 配置容器.
        /// </summary>
        /// <param name="container">配置容器.</param>
        /// <returns>已配置的容器.</returns>
        protected override RegularConfigSource DoConfigure(RegularConfigSource container)
        {
            container.AddHandler(name, handlerKind, sourceType, this.source);
            return container;
        }
        #endregion
    }
}
