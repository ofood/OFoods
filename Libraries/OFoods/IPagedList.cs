
using System.Collections.Generic;

namespace OFoods
{
    /// <summary>
    /// 分页列表界面
    /// </summary>
    public interface IPagedList<T> : IList<T>
    {
        /// <summary>
        /// 页码
        /// </summary>
        int PageIndex { get; }
        /// <summary>
        /// 页码大小
        /// </summary>
        int PageSize { get; }
        /// <summary>
        /// 记录总数
        /// </summary>
        int TotalCount { get; }
        /// <summary>
        /// 总页数
        /// </summary>
        int TotalPages { get; }
        /// <summary>
        /// 是否有上一页
        /// </summary>
        bool HasPreviousPage { get; }
        /// <summary>
        /// 是否有下一页
        /// </summary>
        bool HasNextPage { get; }
    }
}
