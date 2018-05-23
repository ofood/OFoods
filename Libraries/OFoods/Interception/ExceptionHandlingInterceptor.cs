using System;
using Castle.DynamicProxy;
using OFoods.Exceptions;

namespace OFoods.Interception
{
    /// <summary>
    /// 表示处理异常的拦截器.
    /// </summary>
    public class ExceptionHandlingInterceptor : IInterceptor
    {
        /// <summary>
        /// 通过类型获取返回值
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        private object GetReturnValueByType(Type type)
        {
            if (type.IsClass || type.IsInterface)
                return null;
            if (type == typeof(void))
                return null;
            return Activator.CreateInstance(type);
        }
        /// <summary>
        /// 执行拦截动作.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch(Exception ex)
            {
                invocation.ReturnValue = GetReturnValueByType(invocation.Method.ReturnType);
                bool handled= ExceptionManager.HandleException(ex);
                if (!handled)
                    throw;
            }
        }
    }
}
