using OFoods.Domain.Repositories;
using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace OFoods.Repositories.EntityFramework
{
    /// <summary>
    /// 表示实体框架存储库上下文.
    /// </summary>
    public class EntityFrameworkRepositoryContext : RepositoryContext, IEntityFrameworkRepositoryContext
    {
        private readonly DbContext efContext;
        private readonly object sync = new object();

        /// <summary>
        /// 初始化<c> EntityFrameworkRepositoryContext </ c>类的新实例.
        /// </summary>
        /// <param name="efContext">初始化<c> EntityFrameworkRepositoryContext </c>类时使用的<see cref ="DbContext"/>对象.</param>
        public EntityFrameworkRepositoryContext(DbContext efContext)
        {
            this.efContext = efContext;
        }

        #region Protected Methods
        /// <summary>
        /// 销毁对象.
        /// </summary>
        /// <param name="disposing">一个 <see cref ="System.Boolean"/>值，它指示对象是否应释放.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // The dispose method will no longer be responsible for the commit
                // handling. Since the object container might handle the lifetime
                // of the repository context on the WCF per-request basis, users should
                // handle the commit logic by themselves.
                //if (!committed)
                //{
                //    Commit();
                //}
                efContext.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region IEntityFrameworkRepositoryContext Members
        /// <summary>
        /// 获取由实体框架处理的<see cref ="DbContext"/>实例.
        /// </summary>
        public DbContext Context
        {
            get { return this.efContext; }
        }

        #endregion

        #region IRepositoryContext Members
        /// <summary>
        /// 将新对象注册到存储库上下文.
        /// </summary>
        /// <param name="obj">要注册的对象.</param>
        public override void RegisterNew(object obj)
        {
            efContext.Entry(obj).State = EntityState.Added;
            Committed = false;
        }
        /// <summary>
        /// 将修改的对象注册到存储库上下文.
        /// </summary>
        /// <param name="obj">要注册的对象.</param>
        public override void RegisterModified(object obj)
        {
            efContext.Entry(obj).State =EntityState.Modified;
            Committed = false;
        }
        /// <summary>
        /// 将已删除的对象注册到存储库上下文中.
        /// </summary>
        /// <param name="obj">要注册的对象.</param>
        public override void RegisterDeleted(object obj)
        {
            efContext.Entry(obj).State = EntityState.Deleted;
            Committed = false;
        }
        #endregion

        #region IUnitOfWork Members
        /// <summary>
        /// 获取<see cref ="System.Boolean"/>值，该值指示工作单元是否可以支持Microsoft分布式事务处理协调器（MS-DTC）.
        /// </summary>
        public override bool DistributedTransactionSupported
        {
            get { return true; }
        }
        /// <summary>
        /// 提交事物.
        /// </summary>
        public override void Commit()
        {
            if (!Committed)
            {
                lock (sync)
                {
                    efContext.SaveChanges();
                }
                Committed = true;
            }
        }

        public override async Task CommitAsync(CancellationToken cancellationToken)
        {
            if (!Committed)
            {
                await efContext.SaveChangesAsync(cancellationToken);
                Committed = true;
            }
        }

        /// <summary>
        /// 回滚事物
        /// </summary>
        public override void Rollback()
        {
            Committed = false;
        }

        #endregion
    }
}
