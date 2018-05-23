using OFoods.Logging;
using OFoods.Logging.Log4Net;
namespace OFoods.Configurations.Fluent
{
    /// <summary>
    /// OFoods配置类Autofac扩展.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// 使用Log4Net作为记录器.
        /// </summary>
        /// <returns></returns>
        public static Configuration UseLog4Net(this Configuration configuration)
        {
            return UseLog4Net(configuration, "log4net.config");
        }
        /// <summary>
        /// 使用Log4Net作为记录器.
        /// </summary>
        /// <returns></returns>
        public static Configuration UseLog4Net(this Configuration configuration, string configFile, string loggerRepository = "NetStandardRepository")
        {
            configuration.SetDefault<ILoggerFactory, Log4NetLoggerFactory>(new Log4NetLoggerFactory(configFile, loggerRepository));
            return configuration;
        }
    }
}