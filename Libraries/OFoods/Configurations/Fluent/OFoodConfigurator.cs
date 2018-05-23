using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoods.Configurations.Fluent
{
    /// <summary>
    /// 代表OFoods配置器.
    /// </summary>
    public class OFoodsConfigurator:IOFoodsConfigurator
    {
        private readonly RegularConfigSource configSource = new RegularConfigSource();

        /// <summary>
        /// 初始化<c> OFoodConfigurator </ c>类的新实例.
        /// </summary>
        public OFoodsConfigurator() { }

        /// <summary>
        /// 配置容器.
        /// </summary>
        /// <returns>已配置容器.</returns>
        public RegularConfigSource Configure()
        {
            return configSource;
        }
    }
}
