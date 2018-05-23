using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFoods.Specifications;
using Globalstech.Domain.Model;
using System.Linq.Expressions;

namespace Globalstech.Domain.Repositories.Specifications
{
    internal abstract class UserStringEqualsSpecification : Specification<User>
    {
        protected readonly string Value;
        protected UserStringEqualsSpecification(string value)
        {
            this.Value = value;
        }
    }
}
