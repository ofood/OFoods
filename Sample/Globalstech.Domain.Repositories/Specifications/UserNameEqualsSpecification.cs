using System;
using Globalstech.Domain.Model;

namespace Globalstech.Domain.Repositories.Specifications
{
    internal class UserNameEqualsSpecification : UserStringEqualsSpecification
    {
        public UserNameEqualsSpecification(string userName)
            : base(userName)
        {

        }

        public override System.Linq.Expressions.Expression<Func<User, bool>> GetExpression()
        {
            return c => c.UserName == Value;
        }
    }
}
