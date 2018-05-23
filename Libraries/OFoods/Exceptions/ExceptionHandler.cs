using System;

namespace OFoods.Exceptions
{
    /// <summary>
    /// 表示异常处理程序的基类.
    /// </summary>
    /// <typeparam name="TException">正在处理的异常的类型.</typeparam>
    public abstract class ExceptionHandler<TException> : IExceptionHandler
        where TException : Exception
    {
        #region 受保护方法
        /// <summary>
        /// 内部执行异常处理.
        /// </summary>
        /// <param name="ex">要处理的例外.</param>
        /// <returns>如果异常处理成功，则为true，否则为false.</returns>
        protected abstract bool DoHandle(TException ex);
        #endregion

        #region 公共方法
        /// <summary>
        /// 处理特定的异常.
        /// </summary>
        /// <param name="ex">要处理的例外.</param>
        /// <returns>如果成功处理了exceptioin，则为true，否则为false.</returns>
        public virtual bool HandleException(Exception ex)
        {
            return DoHandle(ex as TException);
        }
        #endregion
    }
}
