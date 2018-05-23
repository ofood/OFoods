using System;
using OFoods.Application;

namespace OFoods.Generators
{
    /// <summary>
    /// 表示默认标识生成器.
    /// </summary>
    public sealed class IdentityGenerator : IIdentityGenerator
    {
        private static readonly IdentityGenerator instance = new IdentityGenerator();
        private readonly IIdentityGenerator generator = null;

        static IdentityGenerator() { }

        private IdentityGenerator()
        {
            if (AppRuntime.Instance.CurrentApplication == null)
                throw new OFoodsException("该应用程序尚未初始化并且尚未启动.");
            if (AppRuntime.Instance.CurrentApplication.ConfigSource == null ||
                AppRuntime.Instance.CurrentApplication.ConfigSource.Config == null ||
                AppRuntime.Instance.CurrentApplication.ConfigSource.Config.Generators == null ||
                AppRuntime.Instance.CurrentApplication.ConfigSource.Config.Generators.IdentityGenerator == null ||
                string.IsNullOrEmpty(AppRuntime.Instance.CurrentApplication.ConfigSource.Config.Generators.IdentityGenerator.Provider) ||
                string.IsNullOrWhiteSpace(AppRuntime.Instance.CurrentApplication.ConfigSource.Config.Generators.IdentityGenerator.Provider))
            {
                generator = new SequentialIdentityGenerator();
            }
            else
            {
                Type type = Type.GetType(AppRuntime.Instance.CurrentApplication.ConfigSource.Config.Generators.IdentityGenerator.Provider);
                if (type == null)
                    throw new OFoodsException("无法从名称{0}创建类型.", AppRuntime.Instance.CurrentApplication.ConfigSource.Config.Generators.IdentityGenerator.Provider);
                if (type.Equals(this.GetType()))
                    throw new OFoodsException("类型{0}不能用作标识生成器，它由内部的OFoods框架维护.", this.GetType().AssemblyQualifiedName);

                generator = (IIdentityGenerator)Activator.CreateInstance(type);
            }
        }

        /// <summary>
        /// 获取<c> IdentityGenerator</c>类的实例.
        /// </summary>
        public static IdentityGenerator Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// 生成标识.
        /// </summary>
        /// <returns>生成的标识实例.</returns>
        public object Generate()
        {
            return generator.Generate();
        }
    }
}
