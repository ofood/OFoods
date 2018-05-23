using System;
using Globalstech.Domain.Model;

namespace Globalstech.Domain.Repositories.Specifications
{
    internal class UserPasswordEqualsSpecification : UserStringEqualsSpecification
    {

        public UserPasswordEqualsSpecification(string password)
            : base(password)
        {
        }

        public override System.Linq.Expressions.Expression<Func<User, bool>> GetExpression()
        {
            return c => c.Password == Value;
        }
    }
}
