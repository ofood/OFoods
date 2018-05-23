using OFoods.Domain;
using System;
using System.Collections.Generic;

namespace OFoods.Bus
{
    public interface IBus:IUnitOfWork,IDisposable
    {
        /// <summary>
        /// 将指定的消息发布到总线.
        /// </summary>
        /// <typeparam name="TMessage">要发布的消息的类型.</typeparam>
        /// <param name="message">要发布的消息.</param>
        void Publish<TMessage>(TMessage message);
        /// <summary>
        /// 将一组消息发布到总线.
        /// </summary>
        /// <typeparam name="TMessage">要发布的消息的类型.</typeparam>
        /// <param name="messages">要发布的消息.</param>
        void Publish<TMessage>(IEnumerable<TMessage> messages);
        /// <summary>
        /// 清除等待提交的已发布消息.
        /// </summary>
        void Clear();
    }
}
