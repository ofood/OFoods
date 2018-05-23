using System;
using OFoods.Specifications;
using Globalstech.Domain.Model;
using System.Linq.Expressions;

namespace Globalstech.Domain.Repositories.Specifications
{
    internal class SalesOrderBelongsToUserSpecification : Specification<SalesOrder>
    {
        private readonly User _user;

        public SalesOrderBelongsToUserSpecification(User user)
        {
            _user = user;
        }

        public override Expression<Func<SalesOrder, bool>> GetExpression()
        {
            return so => so.User.ID == _user.ID;
        }
    }
}
