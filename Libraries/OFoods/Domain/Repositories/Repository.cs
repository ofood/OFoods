using OFoods.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OFoods.Domain.Repositories
{
    /// <summary>
    /// 表示存储库的基类.
    /// </summary>
    /// <typeparam name="TKey">聚合根的键的类型.</typeparam>
    /// <typeparam name="TAggregateRoot">聚合根的类型.</typeparam>
    public abstract class Repository<TKey, TAggregateRoot> : IRepository<TKey, TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot<TKey>
        where TKey:IEquatable<TKey>
    {
        private readonly IRepositoryContext context;

        /// <summary>
        /// 初始化<c> Repository<TAggregateRoot> </ c>类的新实例.
        /// </summary>
        /// <param name="context">存储库使用的存储库上下文.</param>
        public Repository(IRepositoryContext context)
        {
            this.context = context;
        }

        #region Protected Methods
        /// <summary>
        /// 将聚合根添加到存储库.
        /// </summary>
        /// <param name="aggregateRoot">要添加到存储库的聚合根.</param>
        protected abstract void DoAdd(TAggregateRoot aggregateRoot);
        /// <summary>
        /// 通过给定密钥从存储库获取聚合根实例.
        /// </summary>
        /// <param name="key">聚合根的关键.</param>
        /// <returns>聚合根的实例.</returns>
        protected abstract TAggregateRoot DoGetByKey(TKey key);
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <returns>所有聚合根都来自存储库.</returns>
        protected virtual IQueryable<TAggregateRoot> DoFindAll()
        {
            return DoFindAll(new AnySpecification<TAggregateRoot>(), null, SortOrder.Unspecified);
        }
        /// <summary>
        /// 从存储库中查找所有聚合根，使用提供的排序谓词和指定的排序顺序进行排序.
        /// </summary>
        /// <param name="sortPredicate">用于排序的排序谓词.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> 指定排序顺序的枚举.</param>
        /// <returns>从存储库获取所有聚合根，并使用提供的排序谓词和排序顺序对聚合根进行排序.</returns>
        protected virtual IQueryable<TAggregateRoot> DoFindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return DoFindAll(new AnySpecification<TAggregateRoot>(), sortPredicate, sortOrder);
        }
        /// <summary>
        /// 查找启用了分页的存储库中的所有聚合根，使用提供的排序谓词和指定的排序顺序进行排序.
        /// </summary>
        /// <param name="sortPredicate">用于排序的排序谓词.</param>
        /// <param name="sortOrder"> <see cref="SortOrder"/> 指定排序顺序的枚举.</param>
        /// <param name="pageNumber">页码.</param>
        /// <param name="pageSize">每页的对象数量.</param>
        /// <returns>从存储库获取所有聚合根，并使用提供的排序谓词和排序顺序对聚合根进行排序.</returns>
        protected virtual PagedResult<TAggregateRoot> DoFindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return DoFindAll(new AnySpecification<TAggregateRoot>(), sortPredicate, sortOrder, pageNumber, pageSize);
        }
        /// <summary>
        /// 查找与给定规范相匹配的所有聚合根.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的规范.</param>
        /// <returns>所有与给定规格相匹配的聚合根.</returns>
        protected virtual IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification)
        {
            return DoFindAll(specification, null, SortOrder.Unspecified);
        }
        /// <summary>
        /// 查找与给定规范匹配的所有聚合根，并使用提供的排序谓词和指定的排序顺序对聚合根进行排序.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的规范.</param>
        /// <param name="sortPredicate">用于排序的排序谓词.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> 指定排序顺序的枚举.</param>
        /// <returns>所有与给定规范相匹配的聚合根，并使用给定的排序谓词和排序顺序进行排序.</returns>
        protected abstract IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder);
        /// <summary>
        /// 在启用分页的情况下查找与给定规范匹配的所有聚合根，并使用提供的排序谓词和指定的排序顺序对聚合根进行排序.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的规范.</param>
        /// <param name="sortPredicate">用于排序的排序谓词.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> 指定排序顺序的枚举.</param>
        /// <param name="pageNumber">每页的对象数量.</param>
        /// <param name="pageSize">每页大小.</param>
        /// <returns>所有与给定规范相匹配的聚合根，并使用给定的排序谓词和排序顺序进行排序.</returns>
        protected abstract PagedResult<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize);
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <param name="eagerLoadingProperties">需要加载的聚合对象的属性.</param>
        /// <returns>聚合根.</returns>
        protected virtual IQueryable<TAggregateRoot> DoFindAll(params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return DoFindAll(new AnySpecification<TAggregateRoot>(), null, SortOrder.Unspecified, eagerLoadingProperties);
        }
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <param name="sortPredicate">用于排序的排序谓词.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> 指定排序顺序的枚举.</param>
        /// <param name="eagerLoadingProperties">需要加载的聚合对象的属性.</param>
        /// <returns>The aggregate root.</returns>
        protected virtual IQueryable<TAggregateRoot> DoFindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return DoFindAll(new AnySpecification<TAggregateRoot>(), sortPredicate, sortOrder, eagerLoadingProperties);
        }
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <param name="sortPredicate">用于排序的排序谓词.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> 指定排序顺序的枚举.</param>
        /// <param name="pageNumber">页码.</param>
        /// <param name="pageSize">每页的对象数量.</param>
        /// <param name="eagerLoadingProperties">需要加载的聚合对象的属性.</param>
        /// <returns>聚合根.</returns>
        protected virtual PagedResult<TAggregateRoot> DoFindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return DoFindAll(new AnySpecification<TAggregateRoot>(), sortPredicate, sortOrder, pageNumber, pageSize, eagerLoadingProperties);
        }
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的规范.</param>
        /// <param name="eagerLoadingProperties">需要加载的聚合对象的属性.</param>
        /// <returns>聚合根.</returns>
        protected virtual IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return DoFindAll(new AnySpecification<TAggregateRoot>(), null, SortOrder.Unspecified, eagerLoadingProperties);
        }
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的规范.</param>
        /// <param name="sortPredicate">用于排序的排序谓词.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> 指定排序顺序的枚举.</param>
        /// <param name="eagerLoadingProperties">需要加载的聚合对象的属性.</param>
        /// <returns>聚合根.</returns>
        protected abstract IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的规范.</param>
        /// <param name="sortPredicate">用于排序的排序谓词.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> 指定排序顺序的枚举.</param>
        /// <param name="pageNumber">页码.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        /// <param name="eagerLoadingProperties">The properties for the aggregated objects that need to be loaded.</param>
        /// <returns>The aggregate root.</returns>
        protected abstract PagedResult<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
        
        /// <summary>
        /// Finds a single aggregate root that matches the given specification.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate root should match.</param>
        /// <returns>The instance of the aggregate root.</returns>
        protected abstract TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification);
        /// <summary>
        /// Finds a single aggregate root from the repository.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate root should match.</param>
        /// <param name="eagerLoadingProperties">The properties for the aggregated objects that need to be loaded.</param>
        /// <returns>The aggregate root.</returns>
        protected abstract TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// Checkes whether the aggregate root, which matches the given specification, exists in the repository.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate root should match.</param>
        /// <returns>True if the aggregate root exists, otherwise false.</returns>
        protected abstract bool DoExists(ISpecification<TAggregateRoot> specification);
        /// <summary>
        /// Removes the aggregate root from current repository.
        /// </summary>
        /// <param name="aggregateRoot">The aggregate root to be removed.</param>
        protected abstract void DoRemove(TAggregateRoot aggregateRoot);
        /// <summary>
        /// Updates the aggregate root in the current repository.
        /// </summary>
        /// <param name="aggregateRoot">The aggregate root to be updated.</param>
        protected abstract void DoUpdate(TAggregateRoot aggregateRoot);

        #endregion

        #region IRepository<TAggregateRoot> Members
        /// <summary>
        /// Gets the <see cref="Apworks.Repositories.IRepositoryContext"/> instance.
        /// </summary>
        public IRepositoryContext Context
        {
            get { return this.context; }
        }
        /// <summary>
        /// Adds an aggregate root to the repository.
        /// </summary>
        /// <param name="aggregateRoot">The aggregate root to be added to the repository.</param>
        public void Add(TAggregateRoot aggregateRoot)
        {
            this.DoAdd(aggregateRoot);
        }
        /// <summary>
        /// Gets the aggregate root instance from repository by a given key.
        /// </summary>
        /// <param name="key">The key of the aggregate root.</param>
        /// <returns>The instance of the aggregate root.</returns>
        public TAggregateRoot GetByKey(TKey key)
        {
            return this.DoGetByKey(key);
        }
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <returns>聚合根.</returns>
        public IQueryable<TAggregateRoot> FindAll()
        {
            return DoFindAll();
        }
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <param name="sortPredicate">用于排序的排序谓词.</param>
        /// <param name="sortOrder">指定排序顺序的<see cref =“SortOrder”/>枚举.</param>
        /// <returns>The aggregate roots.</returns>
        public IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return DoFindAll(sortPredicate, sortOrder);
        }
        /// <summary>
        /// Finds all the aggregate roots that match the given specification.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <returns>The aggregate roots.</returns>
        public IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification)
        {
            return DoFindAll(specification);
        }
        /// <summary>
        /// Finds all the aggregate roots from repository.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="Apworks.Storage.SortOrder"/> enumeration which specifies the sort order.</param>
        /// <returns>The aggregate roots.</returns>
        public IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return DoFindAll(specification, sortPredicate, sortOrder);
        }
        /// <summary>
        /// Finds all the aggregate roots from repository.
        /// </summary>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        /// <returns>The aggregate roots.</returns>
        public PagedResult<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return DoFindAll(sortPredicate, sortOrder, pageNumber, pageSize);
        }
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的规范.</param>
        /// <param name="sortPredicate">用于排序的排序谓词.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> 指定排序顺序的枚举.</param>
        /// <param name="pageNumber">每页的对象数量.</param>
        /// <param name="pageSize">每页的对象大小.</param>
        /// <returns>The aggregate roots.</returns>
        public PagedResult<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return DoFindAll(specification, sortPredicate, sortOrder, pageNumber, pageSize);
        }
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <param name="eagerLoadingProperties">需要加载的聚合对象的属性.</param>
        /// <returns>聚合根.</returns>
        public IQueryable<TAggregateRoot> FindAll(params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return DoFindAll(eagerLoadingProperties);
        }
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <param name="sortPredicate">用于排序的排序谓词.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> 指定排序顺序的枚举.</param>
        /// <param name="eagerLoadingProperties">需要加载的聚合对象的属性.</param>
        /// <returns>聚合根.</returns>
        public IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return DoFindAll(sortPredicate, sortOrder, eagerLoadingProperties);
        }
        /// <summary>
        /// Finds all the aggregate roots from repository.
        /// </summary>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="Apworks.Storage.SortOrder"/> enumeration which specifies the sort order.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        /// <param name="eagerLoadingProperties">The properties for the aggregated objects that need to be loaded.</param>
        /// <returns>The aggregate root.</returns>
        public PagedResult<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return DoFindAll(sortPredicate, sortOrder, pageNumber, pageSize, eagerLoadingProperties);
        }
        /// <summary>
        /// Finds all the aggregate roots from repository.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="eagerLoadingProperties">The properties for the aggregated objects that need to be loaded.</param>
        /// <returns>The aggregate root.</returns>
        public IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(specification, eagerLoadingProperties);
        }
        /// <summary>
        /// Finds all the aggregate roots from repository.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="Apworks.Storage.SortOrder"/> enumeration which specifies the sort order.</param>
        /// <param name="eagerLoadingProperties">The properties for the aggregated objects that need to be loaded.</param>
        /// <returns>The aggregate root.</returns>
        public IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder, eagerLoadingProperties);
        }
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的规范.</param>
        /// <param name="sortPredicate">用于排序的排序谓词.</param>
        /// <param name="sortOrder">指定排序顺序的<see cref ="SortOrder"/>枚举.</param>
        /// <param name="pageNumber">页码.</param>
        /// <param name="pageSize">每页的对象数量.</param>
        /// <param name="eagerLoadingProperties">需要加载的聚合对象的属性.</param>
        /// <returns>The aggregate root.</returns>
        public PagedResult<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder, pageNumber, pageSize, eagerLoadingProperties);
        }
        /// <summary>
        /// 查找与给定规范相匹配的单个聚合根.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的规范.</param>
        /// <returns>聚合根的实例.</returns>
        public TAggregateRoot Find(ISpecification<TAggregateRoot> specification)
        {
            return DoFind(specification);
        }
        /// <summary>
        /// 从存储库中查找单个聚合根.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的规范.</param>
        /// <param name="eagerLoadingProperties">需要加载的聚合对象的属性.</param>
        /// <returns>聚合根.</returns>
        public TAggregateRoot Find(ISpecification<TAggregateRoot> specification, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return DoFind(specification, eagerLoadingProperties);
        }
        /// <summary>
        /// 从当前存储库中删除聚合根.
        /// </summary>
        /// <param name="aggregateRoot">要删除的聚合根.</param>
        public void Remove(TAggregateRoot aggregateRoot)
        {
            DoRemove(aggregateRoot);
        }
        /// <summary>
        /// 更新当前存储库中的聚合根.
        /// </summary>
        /// <param name="aggregateRoot">要更新的聚合根.</param>
        public void Update(TAggregateRoot aggregateRoot)
        {
            DoUpdate(aggregateRoot);
        }
        /// <summary>
        /// 检查存储库中是否存在与给定规范相匹配的聚合根.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的规范.</param>
        /// <returns>如果聚合根存在，则为true，否则为false.</returns>
        public bool Exists(ISpecification<TAggregateRoot> specification)
        {
            return DoExists(specification);
        }
        #endregion
    }

    /// <summary>
    /// 表示实现的类是存储库.
    /// </summary>
    /// <typeparam name="TAggregateRoot">聚合根的类型.</typeparam>
    public abstract class Repository<TAggregateRoot> : Repository<Guid, TAggregateRoot>, IRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        /// <summary>
        /// 初始化<see cref ="Repository {TAggregateRoot}"/>类的新实例.
        /// </summary>
        /// <param name="context">存储库使用的存储库上下文.</param>
        public Repository(IRepositoryContext context)
            : base(context)
        {

        }
    }
}
