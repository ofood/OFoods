using System;

namespace OFoods.Events
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class HandlesAttribute:Attribute
    {
        #region 公共属性
        /// <summary>
        /// 获取或设置可由装饰方法处理的域事件的类型.
        /// </summary>
        public Type DomainEventType { get; set; }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化<c> HandlesAttribute </ c>类的新实例.
        /// </summary>
        /// <param name="domainEventType">可以由装饰方法处理的域事件的类型.</param>
        public HandlesAttribute(Type domainEventType)
        {
            this.DomainEventType = domainEventType;
        }
        #endregion
    }
}
