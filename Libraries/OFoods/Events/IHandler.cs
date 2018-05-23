namespace OFoods.Events
{
    /// <summary>
    /// 表示实现的类是消息处理程序.
    /// </summary>
    /// <typeparam name="T">要处理的消息的类型.</typeparam>
    public interface IHandler<in T>
    {
        /// <summary>
        /// 处理指定的消息.
        /// </summary>
        /// <param name="message">要处理的消息.</param>
        void Handle(T message);
    }
}
