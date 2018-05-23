using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoods.Configurations.Fluent
{
    /// <summary>
    /// 表示序列生成器配置器.
    /// </summary>
    public class SequenceGeneratorConfigurator : TypeSpecifiedConfigSourceConfigurator, ISequenceGeneratorConfigurator
    {
        /// <summary>
        /// 初始化<c> SequenceGeneratorConfigurator </ c>类的新实例.
        /// </summary>
        /// <param name="context">配置上下文.</param>
        /// <param name="sequenceGeneratorType">要在应用程序中使用的生成器的类型.</param>
        public SequenceGeneratorConfigurator(IConfigSourceConfigurator context, Type sequenceGeneratorType)
            : base(context, sequenceGeneratorType)
        { }

        /// <summary>
        /// 配置容器.
        /// </summary>
        /// <param name="container">配置容器.</param>
        /// <returns>已配置容器.</returns>
        protected override RegularConfigSource DoConfigure(RegularConfigSource container)
        {
            container.SequenceGenerator = Type;
            return container;
        }
    }
}
