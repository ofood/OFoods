namespace OFoods.Specifications
{
    /// <summary>
    /// 表示复合规格的基类.
    /// </summary>
    /// <typeparam name="T">应用规范的对象的类型.</typeparam>
    public abstract class CompositeSpecification<T> : Specification<T>, ICompositeSpecification<T>
    {
        #region 私有字段
        private readonly ISpecification<T> left;
        private readonly ISpecification<T> right;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造<c> CompositeSpecification <T> </ c>类的新实例.
        /// </summary>
        /// <param name="left">第一个规范.</param>
        /// <param name="right">第二个规范.</param>
        public CompositeSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            this.left = left;
            this.right = right;
        }
        #endregion

        #region ICompositeSpecification成员
        /// <summary>
        /// 获取第一个规范.
        /// </summary>
        public ISpecification<T> Left
        {
            get { return this.left; }
        }
        /// <summary>
        /// 获取第二个规范
        /// </summary>
        public ISpecification<T> Right
        {
            get { return this.right; }
        }
        #endregion
    }
}
