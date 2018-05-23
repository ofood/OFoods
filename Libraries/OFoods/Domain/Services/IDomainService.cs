using OFoods.Dependency;

namespace OFoods.Domain.Services
{
    /// <summary>
    /// 这个接口必须由所有的域服务来实现，以便按照约定标识它们.
    /// </summary>
    public interface IDomainService : ITransientDependency
    {

    }
}