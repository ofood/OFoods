using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using OFoods.Configurations;
using OFoods.Application;

namespace OFoods.Interception
{
    /// <summary>
    /// 表示拦截器选择器.
    /// </summary>
    public sealed class InterceptorSelector: IInterceptorSelector
    {
        private MethodInfo GetMethodInBase(Type baseType,MethodInfo thisMethod)
        {
            MethodInfo[] methods = baseType.GetMethods();
            var methodQuery = methods.Where(p =>
            {
                var returnVal = p.Name == thisMethod.Name &&
                p.IsGenericMethod == thisMethod.IsGenericMethod &&
                ((p.GetParameters() == null && thisMethod.GetParameters() == null) || (p.GetParameters().Length == thisMethod.GetParameters().Length));
                if (!returnVal)
                    return false;
                var thisMethodParameters = thisMethod.GetParameters();
                var pMethodParameters = p.GetParameters();
                for (int i = 0; i < thisMethodParameters.Length; i++)
                {
                    returnVal &= pMethodParameters[i].ParameterType == thisMethodParameters[i].ParameterType;
                }
                return returnVal;
            });
            if (methodQuery != null && methodQuery.Count() > 0)
                return methodQuery.Single();
            return null;
        }
        /// <summary>
        /// 为给定的类型和方法选择拦截器.
        /// </summary>
        /// <param name="type">类型.</param>
        /// <param name="method">方法.</param>
        /// <param name="interceptors">原始拦截器集合.</param>
        /// <returns>一组特定于给定类型和方法的拦截器.</returns>
        public IInterceptor[] SelectInterceptors(Type type,MethodInfo method, IInterceptor[] interceptors)
        {
            IConfigSource configSource = AppRuntime.Instance.CurrentApplication.ConfigSource;
            List<IInterceptor> selectedInterceptors = new List<IInterceptor>();
            IEnumerable<string> interceptorTypes = configSource.Config.GetInterceptorTypes(type,method);

            if (interceptorTypes == null)
            {
                if (type.BaseType != null && type.BaseType != typeof(Object))
                {
                    Type baseType = type.BaseType;
                    MethodInfo methodInfoBase = null;
                    while (baseType != null && type.BaseType != typeof(Object))
                    {
                        methodInfoBase = GetMethodInBase(baseType, method);
                        if (methodInfoBase != null)
                            break;
                        baseType = baseType.BaseType;
                    }
                    if (baseType != null && methodInfoBase != null)
                    {
                        interceptorTypes = configSource.Config.GetInterceptorTypes(baseType, methodInfoBase);
                    }
                }
                if (interceptorTypes == null)
                {
                    var intfTypes = type.GetInterfaces();
                    if (intfTypes != null && intfTypes.Count() > 0)
                    {
                        foreach (var intfType in intfTypes)
                        {
                            var methodInfoBase = GetMethodInBase(intfType, method);
                            if (methodInfoBase != null)
                                interceptorTypes = configSource.Config.GetInterceptorTypes(intfType, methodInfoBase);
                            if (interceptorTypes != null)
                                break;
                        }
                    }
                }

            }
            if (interceptorTypes != null && interceptorTypes.Count() > 0)
            {
                foreach (var interceptor in interceptors)
                {
                    if (interceptorTypes.Any(p => interceptor.GetType().AssemblyQualifiedName.Equals(p)))
                        selectedInterceptors.Add(interceptor);
                }
            }
            return selectedInterceptors.ToArray();
        }
    }
}
