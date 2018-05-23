using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OFoods.Domain.Repositories
{
    /// <summary>
    /// 表示存储库上下文.
    /// </summary>
    public abstract class RepositoryContext : DisposableObject, IRepositoryContext
    {
        #region 私有字段
        private readonly Guid id = Guid.NewGuid();
        //private readonly ThreadLocal<List<object>> localNewCollection = new ThreadLocal<List<object>>(() => new List<object>());
        //private readonly ThreadLocal<List<object>> localModifiedCollection = new ThreadLocal<List<object>>(() => new List<object>());
        //private readonly ThreadLocal<List<object>> localDeletedCollection = new ThreadLocal<List<object>>(() => new List<object>());
        //private readonly ThreadLocal<bool> localCommitted = new ThreadLocal<bool>(() => true);
        private readonly ConcurrentDictionary<object, byte> newCollection = new ConcurrentDictionary<object, byte>();
        private ConcurrentDictionary<object, byte> modifiedCollection = new ConcurrentDictionary<object, byte>();
        private ConcurrentDictionary<object, byte> deletedCollection = new ConcurrentDictionary<object, byte>();
        private volatile bool committed = true;
        #endregion

        #region 受保护属性
        /// <summary>
        /// 获取枚举器，该枚举器遍历包含需要添加到存储库的所有对象的集合.
        /// </summary>
        protected ConcurrentDictionary<object, byte> NewCollection
        {
            get { return newCollection; }
        }
        /// <summary>
        /// 获取一个枚举器，它遍历包含需要在存储库中修改的所有对象的集合.
        /// </summary>
        protected ConcurrentDictionary<object, byte> ModifiedCollection
        {
            get { return modifiedCollection; }
        }
        /// <summary>
        /// 获取一个枚举器，它遍历包含需要从存储库中删除的所有对象的集合.
        /// </summary>
        protected ConcurrentDictionary<object, byte> DeletedCollection
        {
            get { return deletedCollection; }
        }
        #endregion

        #region 受保护方法
        /// <summary>
        /// 清除存储库上下文中的所有注册.
        /// </summary>
        /// <remarks>请注意，只有在存储库上下文成功提交后才能调用它.</remarks>
        protected void ClearRegistrations()
        {
            newCollection.Clear();
            modifiedCollection.Clear();
            deletedCollection.Clear();
        }
        /// <summary>
        /// 处理该对象.
        /// </summary>
        /// <param name="disposing"><see cref ="System.Boolean"/>值，它指示对象是否应该明确放置.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ClearRegistrations();
            }
        }
        #endregion

        #region IRepositoryContext 成员
        /// <summary>
        /// 获取存储库上下文的ID.
        /// </summary>
        public Guid ID
        {
            get { return id; }
        }
        /// <summary>
        /// 将新对象注册到存储库上下文.
        /// </summary>
        /// <param name="obj">要注册的对象.</param>
        public virtual void RegisterNew(object obj)
        {
            //if (localModifiedCollection.Value.Contains(obj))
            //   throw new InvalidOperationException("The object cannot be registered as a new object since it was marked as modified.");
            //if (localNewCollection.Value.Contains(obj))
            //    throw new InvalidOperationException("The object has already been registered as a new object.");

            //localNewCollection.Value.Add(obj);

            newCollection.AddOrUpdate(obj, byte.MinValue, (o, b) => byte.MinValue);
            Committed = false;
        }
        /// <summary>
        /// 将修改的对象注册到存储库上下文.
        /// </summary>
        /// <param name="obj">要注册的对象.</param>
        public virtual void RegisterModified(object obj)
        {
            //if (localDeletedCollection.Value.Contains(obj))
            //    throw new InvalidOperationException("The object cannot be registered as a modified object since it was marked as deleted.");
            //if (!localModifiedCollection.Value.Contains(obj) && !localNewCollection.Value.Contains(obj))
            //    localModifiedCollection.Value.Add(obj);

            if (deletedCollection.ContainsKey(obj))
                throw new InvalidOperationException("The object cannot be registered as a modified object since it was marked as deleted.");
            if (!modifiedCollection.ContainsKey(obj) && !(newCollection.ContainsKey(obj)))
                modifiedCollection.AddOrUpdate(obj, byte.MinValue, (o, b) => byte.MinValue);

            Committed = false;
        }
        /// <summary>
        /// 将已删除的对象注册到存储库上下文.
        /// </summary>
        /// <param name="obj">要注册的对象.</param>
        public virtual void RegisterDeleted(object obj)
        {
            //if (localNewCollection.Value.Contains(obj))
            //{
            //    if (localNewCollection.Value.Remove(obj))
            //        return;
            //}
            //bool removedFromModified = localModifiedCollection.Value.Remove(obj);
            //bool addedToDeleted = false;
            //if (!localDeletedCollection.Value.Contains(obj))
            //{
            //    localDeletedCollection.Value.Add(obj);
            //    addedToDeleted = true;
            //}
            //localCommitted.Value = !(removedFromModified || addedToDeleted);
            var @byte = byte.MinValue;
            if (newCollection.ContainsKey(obj))
            {
                newCollection.TryRemove(obj, out @byte);
                return;
            }
            var removedFromModified = modifiedCollection.TryRemove(obj, out @byte);
            var addedToDeleted = false;
            if (!deletedCollection.ContainsKey(obj))
            {
                deletedCollection.AddOrUpdate(obj, byte.MinValue, (o, b) => byte.MinValue);
                addedToDeleted = true;
            }
            committed = !(removedFromModified || addedToDeleted);
        }
        #endregion

        #region IUnitOfWork 成员
        /// <summary>
        /// 获取<see cref ="System.Boolean"/>值，该值指示工作单元是否可以支持Microsoft分布式事务处理协调器（MS-DTC）.
        /// </summary>
        public virtual bool DistributedTransactionSupported
        {
            get { return false; }
        }
        /// <summary>
        /// 获取<see cref ="System.Boolean"/>值，该值指示工作单元是否成功提交.
        /// </summary>
        public virtual bool Committed
        {
            get { return committed; }
            protected set { committed = value; }
        }
        /// <summary>
        /// 提交事物.
        /// </summary>
        public abstract void Commit();

        public Task CommitAsync()
        {
            return CommitAsync(CancellationToken.None);
        }

        public abstract Task CommitAsync(CancellationToken cancellationToken);
        /// <summary>
        /// 回滚事物.
        /// </summary>
        public abstract void Rollback();
        #endregion
    }
}
