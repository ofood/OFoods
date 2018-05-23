using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoods.Configurations.Fluent
{
    /// <summary>
    /// 表示所有使用<see cref ="IConfigSource"/>实例作为容器的配置配置器的基类.
    /// </summary>
    public abstract class ConfigSourceConfigurator : Configurator<RegularConfigSource>, IConfigSourceConfigurator
    {
        #region Ctor
        /// <summary>
        /// 初始化<c> ConfigSourceConfigurator </ c>类的新实例.
        /// </summary>
        /// <param name="context">配置上下文.</param>
        public ConfigSourceConfigurator(IConfigSourceConfigurator context)
            : base(context) { }
        #endregion
    }
}
