using OFoods.Domain.Entities;
using System;

namespace OFoods.Events
{
    /// <summary>
    /// 事件接口
    /// </summary>
    public interface IEvent:IEntity
    {
        /// <summary>
        /// 获取或设置事件生成的日期和时间.
        /// </summary>
        /// <remarks>此日期/时间值的格式可能在不同的系统之间是多种多样的。
        /// 推荐系统设计师或建筑师使用标准的UTC时间/日期格式.</remarks>
        DateTime Timestamp { get; set; }
        /// <summary>
        /// 获取或设置事件的程序集限定类型名称.
        /// </summary>
        string AssemblyQualifiedEventType { get; set; }
    }
}
