namespace OFoods.Events
{
    /// <summary>
    /// 表示实现的类是事件处理程序.
    /// </summary>
    /// <typeparam name="TEvent">要处理的事件的类型.</typeparam>
    public interface IEventHandler<in TEvent>:IHandler<TEvent>
        where TEvent:class,IEvent
    {
    }
}
