using System;
using Globalstech.Domain.Model;
using OFoods.Domain.Services;

namespace Globalstech.Domain.Services
{
    public class UserDomainService : DomainService, IUserDomainService
    {
        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
