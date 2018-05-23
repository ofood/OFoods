using OFoods.Events;
using OFoods.Bus;
using Globalstech.Domain.Repositories;
using Globalstech.Domain.Model;

namespace Globalstech.Domain.Events.Handlers
{
    [ParallelExecution]
    public class OrderDispatchedEventHandler : IDomainEventHandler<OrderDispatchedEvent>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly IEventBus _bus;
        public OrderDispatchedEventHandler(ISalesOrderRepository salesOrderRepository, IEventBus bus)
        {
            _salesOrderRepository = salesOrderRepository;
            _bus = bus;
        }
        public void Handle(OrderDispatchedEvent evnt)
        {
            var salesOrder = evnt.Source as SalesOrder;
            if (salesOrder != null)
            {
                salesOrder.DateDispatched = evnt.DispatchedDate;
                salesOrder.Status = SalesOrderStatus.Dispatched;
            }

            _bus.Publish(evnt);
        }
    }
}
