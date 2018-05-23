﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFoods.Events;
using OFoods.Domain.Entities;

namespace Globalstech.Domain.Events
{
    /// <summary>
    /// 表示当针对某销售订单进行发货时所产生的领域事件。
    /// </summary>
    [Serializable]
    public class OrderDispatchedEvent:DomainEvent
    {
        public OrderDispatchedEvent() { }
        public OrderDispatchedEvent(IEntity source) : base(source) { }

        /// <summary>
        /// 获取或设置订单发货的日期。
        /// </summary>
        public DateTime DispatchedDate { get; set; }
        public string UserEmailAddress { get; set; }
        public Guid OrderID { get; set; }
    }
}
