namespace OFoods.Events
{
    /// <summary>
    /// 代表域事件的事件处理程序.
    /// </summary>
    /// <typeparam name="TDomainEvent">当前处理程序要处理的域事件的类型.</typeparam>
    public interface IDomainEventHandler<in TDomainEvent> : IEventHandler<TDomainEvent>
        where TDomainEvent : class, IDomainEvent
    {

    }
}
