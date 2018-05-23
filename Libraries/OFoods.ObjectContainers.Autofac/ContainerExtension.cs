using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OFoods.ObjectContainers.Autofac
{
    public static class ContainerExtension
    {
        public static T[] ResolveAll<T>(this IContainer self)
        {
            return self.Resolve<IEnumerable<T>>().ToArray();
        }

        public static object[] ResolveAll(this IContainer self, Type type)
        {
            Type enumerableOfType = typeof(IEnumerable<>).MakeGenericType(type);
            return (object[])self.ResolveService(new TypedService(enumerableOfType));
        }
        public static T[] ResolveAll<T>(this IContainer self,params object[] argumentsAsAnonymousType)
        {
            return self.Resolve<IEnumerable<T>>(new TypedParameter(argumentsAsAnonymousType.GetType(), argumentsAsAnonymousType)).ToArray();
        }
        public static object[] ResolveAll(this IContainer self,Type type, params object[] argumentsAsAnonymousType)
        {
            Type enumerableOfType = typeof(IEnumerable<>).MakeGenericType(type);
            return (object[])self.ResolveService(new TypedService(enumerableOfType),new TypedParameter(argumentsAsAnonymousType.GetType(), argumentsAsAnonymousType));
        }
    }
}
