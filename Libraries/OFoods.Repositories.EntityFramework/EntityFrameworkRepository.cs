using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using OFoods.Domain;
using OFoods.Domain.Repositories;
using OFoods.Specifications;

namespace OFoods.Repositories.EntityFramework
{
    /// <summary>
    /// 表示实体框架存储库.
    /// </summary>
    /// <typeparam name="TKey">密钥的类型.</typeparam>
    /// <typeparam name="TAggregateRoot">聚合根的类型.</typeparam>
    public class EntityFrameworkRepository<TKey, TAggregateRoot> : Repository<TKey, TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot<TKey>
        where TKey:IEquatable<TKey>
    {
        private readonly IEntityFrameworkRepositoryContext efContext;

        /// <summary>
        /// 初始化<c> Entity Framework Repository </c>类的新实例.
        /// </summary>
        /// <param name="context">存储库上下文.</param>
        public EntityFrameworkRepository(IRepositoryContext context)
            : base(context)
        {
            if (context is IEntityFrameworkRepositoryContext)
                this.efContext = context as IEntityFrameworkRepositoryContext;
        }

        #region Private Methods
        private MemberExpression GetMemberInfo(LambdaExpression lambda)
        {
            if (lambda == null)
                throw new ArgumentNullException("method");

            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr =
                    ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null)
                throw new ArgumentException("method");

            return memberExpr;
        }

        private string GetEagerLoadingPath(Expression<Func<TAggregateRoot, dynamic>> eagerLoadingProperty)
        {
            MemberExpression memberExpression = this.GetMemberInfo(eagerLoadingProperty);
            var parameterName = eagerLoadingProperty.Parameters.First().Name;
            var memberExpressionStr = memberExpression.ToString();
            var path = memberExpressionStr.Replace(parameterName + ".", "");
            return path;
        }
        #endregion

        #region Protected Properties
        /// <summary>
        /// 获取<see cref ="IEntityFrameworkRepositoryContext"/>的实例.
        /// </summary>
        protected IEntityFrameworkRepositoryContext EFContext
        {
            get { return efContext; }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// 将聚合根添加到存储库.
        /// </summary>
        /// <param name="aggregateRoot">要添加到存储库的聚合根.</param>
        protected override void DoAdd(TAggregateRoot aggregateRoot)
        {
            efContext.RegisterNew(aggregateRoot);
        }
        /// <summary>
        /// 通过给定key从存储库获取聚合根实例.
        /// </summary>
        /// <param name="key">聚合根的key.</param>
        /// <returns>聚合根的实例.</returns>
        protected override TAggregateRoot DoGetByKey(TKey key)
        {
            return
                efContext.Context.Set<TAggregateRoot>()
                    .Where(Utils.BuildIdEqualsPredicate<TKey, TAggregateRoot>((TKey)key))
                    .First();
        }
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的规范.</param>
        /// <param name="sortPredicate">用于排序的排序谓词.</param>
        /// <param name="sortOrder">指定排序顺序的<see cref ="SortOrder"/>枚举.</param>
        /// <returns>聚合根.</returns>
        protected override IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification,Expression<Func<TAggregateRoot, dynamic>> sortPredicate,SortOrder sortOrder)
        {
            var query = efContext.Context.Set<TAggregateRoot>().Where(specification.GetExpression());
            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        return query.SortBy<TKey, TAggregateRoot>(sortPredicate);
                    case SortOrder.Descending:
                        return query.SortByDescending<TKey, TAggregateRoot>(sortPredicate);
                    default:
                        break;
                }
            }
            return query;
        }

        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的规范.</param>
        /// <param name="sortPredicate">用于排序的排序谓词.</param>
        /// <param name="sortOrder">指定排序顺序的<see cref ="SortOrder"/>枚举.</param>
        /// <param name="pageNumber">每页的对象数量.</param>
        /// <param name="pageSize">分页大小.</param>
        /// <returns>聚合根.</returns>
        protected override PagedResult<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "The pageNumber is one-based and should be larger than zero.");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "The pageSize is one-based and should be larger than zero.");
            if (sortPredicate == null)
                throw new ArgumentNullException("sortPredicate");

            var query = efContext.Context.Set<TAggregateRoot>()
                .Where(specification.GetExpression());
            int skip = (pageNumber - 1) * pageSize;
            int take = pageSize;

            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    var pagedGroupAscending = query.SortBy<TKey, TAggregateRoot>(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = query.Count() }).FirstOrDefault();
                        if (pagedGroupAscending == null)
                            return null;
                        return new PagedResult<TAggregateRoot>(pagedGroupAscending.Key.Total, (pagedGroupAscending.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber, pagedGroupAscending.Select(p => p).ToList());
                case SortOrder.Descending:
                    var pagedGroupDescending = query.SortByDescending<TKey, TAggregateRoot>(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = query.Count() }).FirstOrDefault();
                        if (pagedGroupDescending == null)
                            return null;
                        return new PagedResult<TAggregateRoot>(pagedGroupDescending.Key.Total, (pagedGroupDescending.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber, pagedGroupDescending.Select(p => p).ToList());
                default:
                    break;
            }

            return null;
        }
        /// <summary>
        /// 从存储库中查找所有聚合根.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的规范.</param>
        /// <param name="sortPredicate">用于排序的排序谓词.</param>
        /// <param name="sortOrder">指定排序顺序的<see cref ="SortOrder"/>枚举.</param>
        /// <param name="eagerLoadingProperties">需要加载的聚合对象的属性.</param>
        /// <returns>聚合根.</returns>
        protected override IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            var dbset = efContext.Context.Set<TAggregateRoot>();
            IQueryable<TAggregateRoot> queryable = null;
            if (eagerLoadingProperties != null &&
                eagerLoadingProperties.Length > 0)
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbset.Include(eagerLoadingPath);
                for (int i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }
                queryable = dbquery.Where(specification.GetExpression());
            }
            else
                queryable = dbset.Where(specification.GetExpression());

            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        return queryable.SortBy<TKey, TAggregateRoot>(sortPredicate);
                    case SortOrder.Descending:
                        return queryable.SortByDescending<TKey, TAggregateRoot>(sortPredicate);
                    default:
                        break;
                }
            }
            return queryable;
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
        /// <returns>聚合根.</returns>
        protected override PagedResult<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "The pageNumber is one-based and should be larger than zero.");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "The pageSize is one-based and should be larger than zero.");
            if (sortPredicate == null)
                throw new ArgumentNullException("sortPredicate");

            int skip = (pageNumber - 1) * pageSize;
            int take = pageSize;

            var dbset = efContext.Context.Set<TAggregateRoot>();
            IQueryable<TAggregateRoot> queryable = null;
            if (eagerLoadingProperties != null &&
                eagerLoadingProperties.Length > 0)
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbset.Include(eagerLoadingPath);
                for (int i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }
                queryable = dbquery.Where(specification.GetExpression());
            }
            else
                queryable = dbset.Where(specification.GetExpression());


            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    var pagedGroupAscending = queryable.SortBy<TKey, TAggregateRoot>(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = queryable.Count() }).FirstOrDefault();
                    if (pagedGroupAscending == null)
                        return null;
                    return new PagedResult<TAggregateRoot>(pagedGroupAscending.Key.Total, (pagedGroupAscending.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber, pagedGroupAscending.Select(p => p).ToList());
                case SortOrder.Descending:
                    var pagedGroupDescending = queryable.SortByDescending<TKey, TAggregateRoot>(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = queryable.Count() }).FirstOrDefault();
                    if (pagedGroupDescending == null)
                        return null;
                    return new PagedResult<TAggregateRoot>(pagedGroupDescending.Key.Total, (pagedGroupDescending.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber, pagedGroupDescending.Select(p => p).ToList());
                default:
                    break;
            }

            return null;
        }
        /// <summary>
        /// 从存储库中查找单个聚合根.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的规范.</param>
        /// <returns>聚合根的实例.</returns>
        protected override TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification)
        {
            return efContext.Context.Set<TAggregateRoot>().Where(specification.IsSatisfiedBy).FirstOrDefault();
        }
        /// <summary>
        /// 从存储库中查找单个聚合根.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的规范.</param>
        /// <param name="eagerLoadingProperties">需要加载的聚合对象的属性.</param>
        /// <returns>聚合根.</returns>
        protected override TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            var dbset = efContext.Context.Set<TAggregateRoot>();
            if (eagerLoadingProperties != null &&
                eagerLoadingProperties.Length > 0)
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbset.Include(eagerLoadingPath);
                for (int i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }
                return dbquery.Where(specification.GetExpression()).FirstOrDefault();
            }
            else
                return dbset.Where(specification.GetExpression()).FirstOrDefault();
        }
        /// <summary>
        /// 检查存储库中是否存在与给定规范相匹配的聚合根.
        /// </summary>
        /// <param name="specification">聚合根应与之匹配的规范.</param>
        /// <returns>如果聚合根存在，则为true，否则为false.</returns>
        protected override bool DoExists(ISpecification<TAggregateRoot> specification)
        {
            var count = efContext.Context.Set<TAggregateRoot>().Count(specification.IsSatisfiedBy);
            return count != 0;
        }
        /// <summary>
        /// 从当前存储库中删除聚合根.
        /// </summary>
        /// <param name="aggregateRoot">要删除的聚合根.</param>
        protected override void DoRemove(TAggregateRoot aggregateRoot)
        {
            efContext.RegisterDeleted(aggregateRoot);
        }
        /// <summary>
        /// 更新当前存储库中的聚合根.
        /// </summary>
        /// <param name="aggregateRoot">要更新的聚合根.</param>
        protected override void DoUpdate(TAggregateRoot aggregateRoot)
        {
            efContext.RegisterModified(aggregateRoot);
        }
        #endregion
    }

    /// <summary>
    /// Represents the Entity Framework repository.
    /// </summary>
    /// <typeparam name="TAggregateRoot">The type of the aggregate root.</typeparam>
    public class EntityFrameworkRepository<TAggregateRoot> : EntityFrameworkRepository<Guid, TAggregateRoot>,
                                                             IRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFrameworkRepository{TAggregateRoot}"/> class.
        /// </summary>
        /// <param name="context">The repository context.</param>
        public EntityFrameworkRepository(IRepositoryContext context)
            : base(context)
        {
        }
    }
}
