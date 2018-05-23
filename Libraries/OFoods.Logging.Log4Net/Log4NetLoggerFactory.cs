using System;
using System.Linq;
using System.IO;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;

namespace OFoods.Logging.Log4Net
{
    /// <summary>基于Log4Net的记录器工厂.
    /// </summary>
    public class Log4NetLoggerFactory : ILoggerFactory
    {
        private readonly string loggerRepository;

        /// <summary>
        /// 参数化的构造函数.
        /// </summary>
        /// <param name="configFile"></param>
        /// <param name="loggerRepository"></param>
        public Log4NetLoggerFactory(string configFile, string loggerRepository = "NetStandardRepository")
        {
            this.loggerRepository = loggerRepository;

            var file = new FileInfo(configFile);
            if (!file.Exists)
            {
                file = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFile));
            }
            var repositories = log4net.LogManager.GetAllRepositories();
            if (repositories != null && repositories.Any(x => x.Name == loggerRepository))
            {
                return;
            }

            var repository = log4net.LogManager.CreateRepository(loggerRepository);
            if (file.Exists)
            {
                XmlConfigurator.ConfigureAndWatch(repository, file);
            }
            else
            {
                BasicConfigurator.Configure(repository, new ConsoleAppender { Layout = new PatternLayout() });
            }

        }
        /// <summary>
        /// 创建一个新的Log4NetLogger实例.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ILogger Create(string name)
        {
            return new Log4NetLogger(log4net.LogManager.GetLogger(loggerRepository, name));
        }
        /// <summary>
        /// 创建一个新的Log4NetLogger实例.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ILogger Create(Type type)
        {
            return new Log4NetLogger(log4net.LogManager.GetLogger(loggerRepository, type));
        }
    }
}
