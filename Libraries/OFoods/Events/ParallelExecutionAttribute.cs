using System;

namespace OFoods.Events
{
    /// <summary>
    /// 表示由此属性装饰的事件处理程序将以并行方式处理事件.
    /// </summary>
    /// <remarks>此属性仅适用于事件处理程序，并且只能由事件总线，事件聚合器或事件调度程序使用。 将此属性应用于其他类型将不起作用.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=false)]
    public class ParallelExecutionAttribute : Attribute
    {

    }
}
