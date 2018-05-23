using System;
using OFoods.Events;
using OFoods.Bus;
using Globalstech.Domain.Model;

namespace Globalstech.Domain.Events.Handlers
{
    [ParallelExecution]
    public class OrderConfirmedEventHandler:IDomainEventHandler<OrderConfirmedEvent>
    {
        private readonly IEventBus _bus;

        public OrderConfirmedEventHandler(IEventBus bus)
        {
            _bus = bus;
        }
        public void Handle(OrderConfirmedEvent evnt)
        {
            var salesOrder = evnt.Source as SalesOrder;
            if (salesOrder != null)
            {
                salesOrder.DateDelivered = evnt.ConfirmedDate;
                salesOrder.Status = SalesOrderStatus.Delivered;
            }

            _bus.Publish(evnt);
        }
    }
}
