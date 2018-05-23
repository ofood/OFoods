using OFoods.Specifications;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace OFoods.Domain.Repositories
{
    public interface IRepository<TKey, TAggregateRoot>
        where TAggregateRoot:class,IAggregateRoot<TKey>  
        where TKey:IEquatable<TKey>
    {
        /// <summary>
        /// 获取存储库所连接的存储库上下文的实例.
        /// </summary>
        IRepositoryContext Context { get; }
        /// <summary>
        /// 将聚合根添加到存储库.
        /// </summary>
        /// <param name="aggregateRoot">要添加到存储库的聚合根.</param>
        void Add(TAggregateRoot aggregateRoot);
        /// <summary>
        /// 通过给定密钥从存储库获取聚合根实例.
        /// </summary>
        /// <param name="key">聚合根的关键.</param>
        /// <returns>聚合根的实例.</returns>
        TAggregateRoot GetByKey(TKey key);
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <returns>聚合根.</returns>
        IQueryable<TAggregateRoot> FindAll();
        
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <param name="sortPredicate">用于排序的排序谓词.</param>
        /// <param name="sortOrder">指定排序顺序的<see cref ="SortOrder"/>枚举.</param>
        /// <returns>聚合根.</returns>
        IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder);
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <param name="sortPredicate">用于排序的排序谓词.</param>
        /// <param name="sortOrder">指定排序顺序的<see cref ="SortOrder"/>枚举.</param>
        /// <param name="pageNumber">页码.</param>
        /// <param name="pageSize">页面大小.</param>
        /// <returns>聚合根.</returns>
        PagedResult<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize);
        /// <summary>
        /// 查找与给定条件相匹配的所有聚合根.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的条件.</param>
        /// <returns>聚合根.</returns>
        IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification);
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的条件.</param>
        /// <param name="sortPredicate">用于排序的排序谓词.</param>
        /// <param name="sortOrder">指定排序顺序的<see cref ="OFood.Specifications.SortOrder"/>枚举.</param>
        /// <returns>聚合根.</returns>
        IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder);
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的条件.</param>
        /// <param name="sortPredicate">用于排序的排序谓词.</param>
        /// <param name="sortOrder">指定排序顺序的<see cref ="SortOrder"/>枚举.</param>
        /// <param name="pageNumber">页码.</param>
        /// <param name="pageSize">每页大小.</param>
        /// <returns>The aggregate roots.</returns>
        PagedResult<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize);
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <param name="eagerLoadingProperties">需要加载的聚合对象的属性.</param>
        /// <returns>聚合根.</returns>
        IQueryable<TAggregateRoot> FindAll(params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <param name="sortPredicate">用于排序的排序谓词.</param>
        /// <param name="sortOrder">指定排序顺序的<see cref ="SortOrder"/>枚举.</param>
        /// <param name="eagerLoadingProperties">需要加载的聚合对象的属性.</param>
        /// <returns>聚合根.</returns>
        IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <param name="sortPredicate">用于排序的排序谓词.</param>
        /// <param name="sortOrder">指定排序顺序的<see cref ="SortOrder"/>枚举.</param>
        /// <param name="pageNumber">页码.</param>
        /// <param name="pageSize">每页大小.</param>
        /// <param name="eagerLoadingProperties">需要加载的聚合对象的属性.</param>
        /// <returns>聚合根.</returns>
        PagedResult<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的条件.</param>
        /// <param name="eagerLoadingProperties">需要加载的聚合对象的属性.</param>
        /// <returns>聚合根.</returns>
        IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的条件.</param>
        /// <param name="sortPredicate">用于排序的排序谓词.</param>
        /// <param name="sortOrder">指定排序顺序的<see cref ="OFood.Specifications.SortOrder"/>枚举.</param>
        /// <param name="eagerLoadingProperties">需要加载的聚合对象的属性.</param>
        /// <returns>聚合根.</returns>
        IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的规范.</param>
        /// <param name="sortPredicate">用于排序的排序谓词.</param>
        /// <param name="sortOrder">指定排序顺序的<see cref ="OFood.Specifications.SortOrder"/>枚举.</param>
        /// <param name="pageNumber">页码.</param>
        /// <param name="pageSize">每页大小.</param>
        /// <param name="eagerLoadingProperties">需要加载的聚合对象的属性.</param>
        /// <returns>The aggregate root.</returns>
        PagedResult<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// 从存储库中查找单个聚合根.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的规范.</param>
        /// <returns>聚合根的实例.</returns>
        TAggregateRoot Find(ISpecification<TAggregateRoot> specification);
        /// <summary>
        /// 从存储库中查找单个聚合根.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的规范.</param>
        /// <param name="eagerLoadingProperties">需要加载的聚合对象的属性.</param>
        /// <returns>聚合根.</returns>
        TAggregateRoot Find(ISpecification<TAggregateRoot> specification, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
        /// <summary>
        /// 检查存储库中是否存在与给定规范相匹配的聚合根.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的规范.</param>
        /// <returns>如果聚合根存在，则为true，否则为false.</returns>
        bool Exists(ISpecification<TAggregateRoot> specification);
        /// <summary>
        /// 从当前存储库中删除聚合根.
        /// </summary>
        /// <param name="aggregateRoot">要删除的聚合根.</param>
        void Remove(TAggregateRoot aggregateRoot);
        /// <summary>
        /// 更新当前存储库中的聚合根.
        /// </summary>
        /// <param name="aggregateRoot">要更新的聚合根.</param>
        void Update(TAggregateRoot aggregateRoot);
    }
    public interface IRepository<TAggregateRoot> : IRepository<Guid, TAggregateRoot>
     where TAggregateRoot : class, IAggregateRoot<Guid>
    {

    }
}
