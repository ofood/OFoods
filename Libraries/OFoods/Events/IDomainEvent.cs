using OFoods.Domain.Entities;


namespace OFoods.Events
{
    /// <summary>
    /// 代表一个领域事件.
    /// </summary>
    public interface IDomainEvent : IEvent
    {
        /// <summary>
        /// 获取或设置从中生成域事件的源实体
        /// </summary>
        IEntity Source { get; set; }

        /// <summary>
        /// 获取或设置领域事件的版本.
        /// </summary>
        long Version { get; set; }
        /// <summary>
        /// 获取或设置当前领域事件所在的分支.
        /// </summary>
        long Branch { get; set; }
    }
}
