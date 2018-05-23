using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Globalstech.Domain.Model;

namespace Globalstech.Domain.Repositories.Specifications
{
    internal class UserEmailEqualsSpecification : UserStringEqualsSpecification
    {
        public UserEmailEqualsSpecification(string email) : base(email)
        {

        }
        public override Expression<Func<User, bool>> GetExpression()
        {
            return c=>c.Email==Value;
        }
    }
}
