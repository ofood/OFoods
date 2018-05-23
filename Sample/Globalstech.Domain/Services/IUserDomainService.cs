using Globalstech.Domain.Model;
using OFoods.Domain.Services;

namespace Globalstech.Domain.Services
{
    public interface IUserDomainService:IDomainService
    {
        User GetUserById(int id);
    }
}
