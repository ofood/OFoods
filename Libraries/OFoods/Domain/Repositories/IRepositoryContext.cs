
using System;

namespace OFoods.Domain.Repositories
{
    public interface IRepositoryContext:IUnitOfWork,IDisposable
    {
        /// <summary>
        /// 获取存储库上下文的唯一标识符.
        /// </summary>
        Guid ID { get; }
        /// <summary>
        /// 将新对象注册到存储库上下文.
        /// </summary>
        /// <param name="obj">要注册的对象.</param>
        void RegisterNew(object obj);
        /// <summary>
        /// 将修改的对象注册到存储库上下文.
        /// </summary>
        /// <param name="obj">要注册的对象.</param>
        void RegisterModified(object obj);
        /// <summary>
        /// 将已删除的对象注册到存储库上下文.
        /// </summary>
        /// <param name="obj">要注册的对象.</param>
        void RegisterDeleted(object obj);
    }
}
