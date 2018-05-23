using System;
using OFoods.Specifications;
using Globalstech.Domain.Model;

namespace Globalstech.Domain.Repositories.Specifications
{
    public class SalesOrderIDEqualsSpecification : Specification<SalesOrder>
    {
        private readonly Guid _orderID;

        public SalesOrderIDEqualsSpecification(Guid orderID)
        {
            _orderID = orderID;
        }

        public override System.Linq.Expressions.Expression<Func<SalesOrder, bool>> GetExpression()
        {
            return p => p.ID == _orderID;
        }
    }
}
