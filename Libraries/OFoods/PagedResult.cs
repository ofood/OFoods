using System;
using System.Collections;
using System.Collections.Generic;


namespace OFoods
{
    /// <summary>
    /// 表示一个集合，其中包含来自整个对象集的特定页面的一组对象.
    /// </summary>
    /// <typeparam name="T">对象的类型.</typeparam>
    [Serializable]
    public class PagedResult<T> : ICollection<T>
    {
        #region 构造函数
        /// <summary>
        /// 初始化<c> PagedResult </c>类的新实例.
        /// </summary>
        public PagedResult()
        {
            data = new List<T>();
        }
        /// <summary>
        /// 初始化<c> PagedResult </c>类的新实例.
        /// </summary>
        /// <param name="totalRecords">整个对象集中包含的记录总数.</param>
        /// <param name="totalPages">总页数.</param>
        /// <param name="pageSize">每页记录的数量.</param>
        /// <param name="pageNumber">当前页码.</param>
        /// <param name="data">包含在当前页面中的对象.</param>
        public PagedResult(int? totalRecords, int? totalPages, int? pageSize, int? pageNumber, IList<T> data)
        {
            this.totalRecords = totalRecords;
            this.totalPages = totalPages;
            this.pageSize = pageSize;
            this.pageNumber = pageNumber;
            this.data = data;
        }
        #endregion

        #region 公共属性
        private int? totalRecords;
        /// <summary>
        /// 获取或设置记录的总数.
        /// </summary>
        public int? TotalRecords
        {
            get { return totalRecords; }
            set { totalRecords = value; }
        }

        private int? totalPages;
        /// <summary>
        /// 获取或设置可用的总页数.
        /// </summary>
        public int? TotalPages
        {
            get { return totalPages; }
            set { totalPages = value; }
        }

        private int? pageSize;
        /// <summary>
        /// 获取或设置每个页面的记录数.
        /// </summary>
        public int? PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        private int? pageNumber;
        /// <summary>
        /// 获取或设置页码.
        /// </summary>
        public int? PageNumber
        {
            get { return pageNumber; }
            set { pageNumber = value; }
        }

        private IList<T> data;
        /// <summary>
        /// 获取当前<c> PagedResult{T}</ c>对象包含的对象列表.
        /// </summary>
        public IEnumerable<T> Data
        {
            get { return data; }
        }
        #endregion

        #region ICollection<T> 成员
        /// <summary>
        /// 将一个项目添加到System.Collections.Generic.ICollection {T}.
        /// </summary>
        /// <param name="item">要添加到System.Collections.Generic.ICollection {T}的对象.</param>
        public void Add(T item)
        {
            data.Add(item);
        }
        /// <summary>
        /// 从System.Collections.Generic.ICollection {T}中删除所有项目.
        /// </summary>
        public void Clear()
        {
            data.Clear();
        }
        /// <summary>
        /// 确定System.Collections.Generic.ICollection {T}是否包含特定值.
        /// </summary>
        /// <param name="item">要在System.Collections.Generic.ICollection {T}中找到的对象.</param>
        /// <returns>如果在System.Collections.Generic.ICollection {T}中找到item，则返回true;否则为false.</returns>
        public bool Contains(T item)
        {
            return data.Contains(item);
        }
        /// <summary>
        /// 将System.Collections.Generic.ICollection {T}的元素复制到System.Array，从一个特定的System.Array索引开始.
        /// </summary>
        /// <param name="array">一维System.Array，它是从System.Collections.Generic.ICollection {T}复制的元素的目的地。 System.Array必须具有从零开始的索引.</param>
        /// <param name="arrayIndex">从数组开始复制的从零开始的索引.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            data.CopyTo(array, arrayIndex);
        }
        /// <summary>
        /// 获取System.Collections.Generic.ICollection {T}中包含的元素的数量.
        /// </summary>
        public int Count
        {
            get { return data.Count; }
        }
        /// <summary>
        /// 获取一个值，该值指示System.Collections.Generic.ICollection {T}是否是只读的.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }
        /// <summary>
        /// 从System.Collections.Generic.ICollection {T}中删除第一次出现的特定对象.
        /// </summary>
        /// <param name="item">要从System.Collections.Generic.ICollection {T}中移除的对象.</param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            return data.Remove(item);
        }

        #endregion

        #region IEnumerable<T> 成员
        /// <summary>
        /// 返回遍历集合的枚举器.
        /// </summary>
        /// <returns>可用于遍历集合的System.Collections.Generic.IEnumerator {T}.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员
        /// <summary>
        /// 返回遍历集合的枚举数.
        /// </summary>
        /// <returns>可用于遍历集合的System.Collections.IEnumerator对象.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        #endregion
    }
}
