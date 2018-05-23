using System.Threading;
using System.Threading.Tasks;

namespace OFoods.Domain
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 获取<see cref ="System.Boolean"/>值，该值指示工作单元是否可以支持Microsoft分布式事务处理协调器（MS-DTC）
        /// </summary>
        bool DistributedTransactionSupported { get; }
        /// <summary>
        /// 获取<see cref ="System.Boolean"/>值，该值指示工作单元是否成功提交.
        /// </summary>
        bool Committed { get; }
        /// <summary>
        /// 提交事物.
        /// </summary>
        void Commit();
        /// <summary>
        /// 异步提交事务.
        /// </summary>
        /// <returns>执行提交操作的任务.</returns>
        Task CommitAsync();
        /// <summary>
        /// 异步提交事务.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> object which propagates notification that operations should be canceled.</param>
        /// <returns>执行提交操作的任务.</returns>
        Task CommitAsync(CancellationToken cancellationToken);
        /// <summary>
        /// 回滚事务.
        /// </summary>
        void Rollback();
    }
}
