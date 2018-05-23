using System;

namespace OFoods.Exceptions
{
    /// <summary>
    /// 表示实现的类是异常处理程序.
    /// </summary>
    public interface IExceptionHandler
    {
        /// <summary>
        /// 处理特定的异常.
        /// </summary>
        /// <param name="ex">要处理的例外.</param>
        /// <returns>如果成功处理了exceptioin，则为true，否则为false.</returns>
        bool HandleException(Exception ex);
    }
}
