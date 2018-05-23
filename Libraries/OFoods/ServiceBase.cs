
using OFoods.Logging;

namespace OFoods
{
    /// <summary>
    /// 这个类可以用作服务的基类.
    /// 它有一些有用的对象属性注入，并且有一些基本的方法可能需要大多数服务.
    /// </summary>
    public abstract class ServiceBase
    {
        /// <summary>
        /// 引用记录器来写入日志.
        /// </summary>
        public ILogger Logger { protected get; set; }

    }
}
