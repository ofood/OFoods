using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Collections.Generic;
using OFoods.Domain.Entities;
using System.Threading.Tasks;
using OFoods.Utility;
using System.Linq;
using OFoods.Dependency;

namespace OFoods.Events
{
    /// <summary>
    /// 表示一个抽象的通用域事件.
    /// </summary>
    [Serializable]
    public abstract class DomainEvent : IDomainEvent
    {
        #region 构造函数
        /// <summary>
        /// 初始化<c> DomainEvent </ c>类的新实例.
        /// </summary>
        public DomainEvent() { }
        /// <summary>
        /// 初始化<c> DomainEvent </ c>类的新实例.
        /// </summary>
        /// <param name="source">引发域事件的源实体.</param>
        public DomainEvent(IEntity source)
        {
            this.Source = source;
        }
        #endregion

        #region 公用方法
        /// <summary>
        /// 返回当前域事件的哈希码.
        /// </summary>
        /// <returns>计算当前域事件的哈希码.</returns>
        public override int GetHashCode()
        {
            return UtilsHelper.GetHashCode(this.Source.GetHashCode(),
                this.Branch.GetHashCode(),
                this.ID.GetHashCode(),
                this.Timestamp.GetHashCode(),
                this.Version.GetHashCode());
        }
        /// <summary>
        /// 返回一个<see cref ="System.Boolean"/>值，指示该实例是否等于指定的实体.
        /// </summary>
        /// <param name="obj">与此实例进行比较的对象.</param>
        /// <returns>如果obj是<see cref ="Apworks.ISourcedAggregateRoot"/> type的实例并且等于此实例的值，则为true; 否则，是错误的.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj == null)
                return false;
            DomainEvent other = obj as DomainEvent;
            if ((object)other == (object)null)
                return false;
            return this.ID == other.ID;
        }
        #endregion

        #region IDomainEvent 成员
        /// <summary>
        /// 获取或设置从中生成域事件的源实体.
        /// </summary>
        [XmlIgnore]
        [SoapIgnore]
        [IgnoreDataMember]
        public IEntity Source
        {
            get;
            set;
        }
        /// <summary>
        /// 获取或设置域事件的版本.
        /// </summary>
        public virtual long Version { get; set; }
        /// <summary>
        /// 获取或设置当前版本的域事件所在的分支.
        /// </summary>
        public virtual long Branch { get; set; }
        /// <summary>
        /// 获取或设置事件的程序集限定类型名称.
        /// </summary>
        public virtual string AssemblyQualifiedEventType { get; set; }
        #endregion

        #region IEvent 成员
        /// <summary>
        /// 获取或设置事件产生的日期和时间.
        /// </summary>
        /// <remarks>这个日期/时间值的格式在不同系统之间可能是不同的。 
        /// 建议系统设计师或建筑师使用标准的UTC日期/时间格式.</remarks>
        public virtual DateTime Timestamp { get; set; }
        #endregion

        #region IEntity 成员
        /// <summary>
        /// 获取或设置域事件的标识符.
        /// </summary>
        public virtual Guid ID { get; set; }
        #endregion
       
        #region 公共静态方法
        
        /// <summary>
        /// 将域事件发布到注册的域事件处理程序.
        /// </summary>
        /// <typeparam name="TDomainEvent">要发布的域事件的类型.</typeparam>
        /// <param name="domainEvent">要发布的域名事件.</param>
        /// <remarks>
        /// 此方法将域事件发布到已注册到对象容器的域事件处理程序。 
        /// 该方法将使用<see cref ="ServiceLocator"/>实例来解析所有注册的域事件处理程序，然后将给定的域事件发布到所有这些注册的处理程序。 
        /// 域事件处理程序应实现接口<see cref ="IDomainEventHandler {T}"/>.
        /// </remarks>
        public static void Publish<TDomainEvent>(TDomainEvent domainEvent)
            where TDomainEvent : class, IDomainEvent
        {
            IEnumerable<IDomainEventHandler<TDomainEvent>> handlers = IocResolver
                .Instance
                .ResolveAll<IDomainEventHandler<TDomainEvent>>();
            foreach (var handler in handlers)
            {
                if (handler.GetType().IsDefined(typeof(ParallelExecutionAttribute), false))
                    Task.Factory.StartNew(() => handler.Handle(domainEvent));
                else
                    handler.Handle(domainEvent);
            }
        }
        /// <summary>
        /// Publishes the domain event to the registered domain event handlers.
        /// </summary>
        /// <typeparam name="TDomainEvent">The type of the domain event to be published.</typeparam>
        /// <param name="domainEvent">The domain event to be published.</param>
        /// <param name="callback">The callback function which will be executed after the
        /// domain event has been published and processed.</param>
        /// <param name="timeout">If a domain event handler is decorated by <see cref="ParallelExecutionAttribute"/> attribute, this parameter
        /// is to specify the timeout value for the handler to process the event.</param>
        /// <remarks>
        /// This method publishes domain events to the domain event handlers that have been registered 
        /// to the object container. The method will use the <see cref="ServiceLocator"/> instance to
        /// resolve all the registered domain event handlers, then publish the given domain event to
        /// all of these registered handlers. The domain event handler should implement the interface
        /// <see cref="IDomainEventHandler{T}"/>.
        /// </remarks>
        public static void Publish<TDomainEvent>(TDomainEvent domainEvent, Action<TDomainEvent, bool, Exception> callback, TimeSpan? timeout = null)
            where TDomainEvent : class, IDomainEvent
        {
            IEnumerable<IDomainEventHandler<TDomainEvent>> handlers = IocResolver
                .Instance
                .ResolveAll<IDomainEventHandler<TDomainEvent>>();
            if (handlers != null && handlers.Count() > 0)
            {
                List<Task> tasks = new List<Task>();
                try
                {
                    foreach (var handler in handlers)
                    {
                        if (handler.GetType().IsDefined(typeof(ParallelExecutionAttribute), false))
                        {
                            tasks.Add(Task.Factory.StartNew(() => handler.Handle(domainEvent)));
                        }
                        else
                            handler.Handle(domainEvent);
                    }
                    if (tasks.Count > 0)
                    {
                        if (timeout == null)
                            Task.WaitAll(tasks.ToArray());
                        else
                            Task.WaitAll(tasks.ToArray(), timeout.Value);
                    }
                    callback(domainEvent, true, null);
                }
                catch (Exception ex)
                {
                    callback(domainEvent, false, ex);
                }
            }
            else
                callback(domainEvent, false, null);
        }
       
        #endregion
    }
}
 