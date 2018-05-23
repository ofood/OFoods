using Globalstech.Domain.Model;
using OFoods.Repositories.EntityFramework;
using Globalstech.Domain.Repositories.Specifications;
using OFoods.Specifications;
namespace Globalstech.Domain.Repositories.EntityFramework
{
    public class UserRepository: EntityFrameworkRepository<User>, IUserRepository
    {
        public UserRepository(IEntityFrameworkRepositoryContext context):base(context)
        {

        }

        public bool CheckPassword(string userName, string password)
        {
            
            return Exists(new AndSpecification<User>(new UserNameEqualsSpecification(userName),new UserPasswordEqualsSpecification(password)));
        }

        public bool EmailExists(string email)
        {
            return Exists(new UserEmailEqualsSpecification(email));
        }

        public User GetUserByEmail(string email)
        {
            return Find(new UserEmailEqualsSpecification(email));
        }

        public User GetUserByName(string userName)
        {
            return Find(new UserNameEqualsSpecification(userName));
        }

        public bool UserNameExists(string userName)
        {
            return Exists(new UserNameEqualsSpecification(userName));
        }
    }
}
