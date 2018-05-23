using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFoods.Domain.Entities;
using Globalstech.Domain.Model;
using OFoods.Events;

namespace Globalstech.Domain.Events
{
    [Serializable]
    public class GetUserOrdersEvent : DomainEvent
    {
        public GetUserOrdersEvent(IEntity source) : base(source) { }

        public IEnumerable<SalesOrder> SalesOrders { get; set; }
    }
}
