using OFoods.ObjectContainers.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoods.Configurations.Fluent
{
    /// <summary>
    /// 表示扩展方法提供程序，它基于现有的OFoods Fluent API例程为使用Autofac容器提供其他API.
    /// </summary>
    public static class AutofacContainerFluentExtender
    {
        /// <summary>
        /// 缺省情况下，使用Autofac作为对象容器和其他设置来配置OFoods框架.
        /// </summary>
        public static IObjectContainerConfigurator UseAutofacContainerWithDefaultSettings(this IOFoodsConfigurator configurator, bool containerInitFromConfigFile = false, string containerConfigPath = null)
        {
            return configurator.WithDefaultSettings().UseAutofacContainer(containerInitFromConfigFile, containerConfigPath);
        }
        /// <summary>
        /// 通过使用Autofac作为对象容器来配置OFoods框架.
        /// </summary>
        public static IObjectContainerConfigurator UseAutofacContainer(this ISequenceGeneratorConfigurator configurator, bool initFromConfigFile = false, string configPath = null)
        {
            return configurator.UsingObjectContainer<AutofacObjectContainer>(initFromConfigFile, configPath);
        }
        /// <summary>
        /// 通过使用Autofac作为对象容器来配置OFoods框架.
        /// </summary>
        public static IObjectContainerConfigurator UseAutofacContainer(this IHandlerConfigurator configurator, bool initFromConfigFile = false, string configPath = null)
        {
            return configurator.UsingObjectContainer<AutofacObjectContainer>(initFromConfigFile, configPath);
        }
        /// <summary>
        /// 通过使用Autofac作为对象容器来配置OFoods框架.
        /// </summary>
        public static IObjectContainerConfigurator UseAutofacContainer(this IExceptionHandlerConfigurator configurator, bool initFromConfigFile = false, string configPath = null)
        {
            return configurator.UsingObjectContainer<AutofacObjectContainer>(initFromConfigFile, configPath);
        }
        /// <summary>
        /// 通过使用Autofac作为对象容器来配置OFoods框架.
        /// </summary>
        public static IObjectContainerConfigurator UseAutofacContainer(this IInterceptionConfigurator configurator, bool initFromConfigFile = false, string configPath = null)
        {
            return configurator.UsingObjectContainer<AutofacObjectContainer>(initFromConfigFile, configPath);
        }
    }
}
