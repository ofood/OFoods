
using System.Data.Entity;
using OFoods.Domain.Repositories;

namespace OFoods.Repositories.EntityFramework
{
    /// <summary>
    /// 表示实现的类是使用Entity Framework提供的功能的存储库上下文.
    /// </summary>
    public interface IEntityFrameworkRepositoryContext : IRepositoryContext
    {
        /// <summary>
        /// 获取由实体框架处理的<see cref ="DbContext"/>实例.
        /// </summary>
        DbContext Context { get; }
    }
}
