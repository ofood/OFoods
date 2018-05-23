using System;
using System.Runtime.ConstrainedExecution;

namespace OFoods
{
    /// <summary>
    /// 表示派生类是可回收对象.
    /// </summary>
    public abstract class DisposableObject : CriticalFinalizerObject, IDisposable
    {
        #region 析构函数
        /// <summary>
        /// 销毁对象.
        /// </summary>
        ~DisposableObject()
        {
            this.Dispose(false);
        }
        #endregion

        #region 受保护方法
        /// <summary>
        /// 处理该对象.
        /// </summary>
        /// <param name="disposing">A <see cref="System.Boolean"/> value which indicates whether
        /// the object should be disposed explicitly.</param>
        protected abstract void Dispose(bool disposing);
        /// <summary>
        /// Provides the facility that disposes the object in an explicit manner,
        /// preventing the Finalizer from being called after the object has been
        /// disposed explicitly.
        /// </summary>
        protected void ExplicitDispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region IDisposable Members
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or
        /// resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.ExplicitDispose();
        }
        #endregion
    }
}
