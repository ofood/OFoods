using System;
using System.Configuration;
using OFoods.Application;
using OFoods.Properties;

namespace OFoods.Generators
{
    /// <summary>
    /// 表示默认的序列生成器.
    /// </summary>
    public sealed class SequenceGenerator : ISequenceGenerator
    {

        private static readonly SequenceGenerator instance = new SequenceGenerator();
        private readonly ISequenceGenerator generator = null;

        static SequenceGenerator() { }

        private SequenceGenerator()
        {
            try
            {
                if (AppRuntime.Instance.CurrentApplication == null)
                    throw new OFoodsException("该应用程序尚未初始化并且尚未启动.");

                if (AppRuntime.Instance.CurrentApplication.ConfigSource == null ||
                    AppRuntime.Instance.CurrentApplication.ConfigSource.Config == null ||
                    AppRuntime.Instance.CurrentApplication.ConfigSource.Config.Generators == null ||
                    AppRuntime.Instance.CurrentApplication.ConfigSource.Config.Generators.SequenceGenerator == null ||
                    string.IsNullOrEmpty(AppRuntime.Instance.CurrentApplication.ConfigSource.Config.Generators.SequenceGenerator.Provider) ||
                    string.IsNullOrWhiteSpace(AppRuntime.Instance.CurrentApplication.ConfigSource.Config.Generators.SequenceGenerator.Provider))
                {
                    generator = new SequentialIdentityGenerator();
                }
                else
                {
                    Type type = Type.GetType(AppRuntime.Instance.CurrentApplication.ConfigSource.Config.Generators.SequenceGenerator.Provider);
                    if (type == null)
                        throw new OFoodsException(string.Format("无法从名称{0}创建类型.", AppRuntime.Instance.CurrentApplication.ConfigSource.Config.Generators.SequenceGenerator.Provider));
                    if (type.Equals(this.GetType()))
                        throw new OFoodsException("类型{0}不能用作序列发生器，它由内部OFoods框架维护.", this.GetType().AssemblyQualifiedName);

                    generator = (ISequenceGenerator)Activator.CreateInstance(type);
                }
            }
            catch (ConfigurationErrorsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new OFoodsException(Resources.EX_GET_IDENTITY_GENERATOR_FAIL, ex);
            }
        }

        /// <summary>
        /// 获取<c> SequenceGenerator</c>类的单例实例.
        /// </summary>
        public static SequenceGenerator Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// 获取序列的下一个值.
        /// </summary>
        public object Next
        {
            get { return generator.Next; }
        }
    }
}
