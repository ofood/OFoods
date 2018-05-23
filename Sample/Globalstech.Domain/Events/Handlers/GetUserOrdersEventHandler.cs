using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFoods.Events;
using OFoods.Bus;
using Globalstech.Domain.Repositories;
using Globalstech.Domain.Model;

namespace Globalstech.Domain.Events.Handlers
{
    [ParallelExecution]
    public class GetUserOrdersEventHandler : IDomainEventHandler<GetUserOrdersEvent>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        public GetUserOrdersEventHandler(ISalesOrderRepository salesOrderRepository)
        {
            _salesOrderRepository = salesOrderRepository;
        }
        public void Handle(GetUserOrdersEvent evnt)
        {
            var user = evnt.Source as User;
            evnt.SalesOrders = _salesOrderRepository.FindSalesOrdersByUser(user);
        }
    }
}
