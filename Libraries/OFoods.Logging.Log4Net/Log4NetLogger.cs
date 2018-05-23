using System;
using log4net;
using log4net.Core;
using OFoods.Utility.Extensions;

namespace OFoods.Logging.Log4Net
{
    /// <summary>
    /// 基于Log4Net的记录器实现.
    /// </summary>
    public class Log4NetLogger : LogBase
    {
        private static readonly Type DeclaringType = typeof(Log4NetLogger);
        private readonly log4net.Core.ILogger _log;
        /// <summary>参数化的构造函数.
        /// </summary>
        /// <param name="log"></param>
        public Log4NetLogger(ILoggerWrapper wrapper)
        {
            _log = wrapper.Logger;
        }
        #region 属性
        public override bool IsDebugEnabled
        {
            get { return _log.IsEnabledFor(Level.Debug); }
        }

        public override bool IsDataLogging
        {
            get { return false; }
        }

        public override bool IsTraceEnabled
        {
            get { return _log.IsEnabledFor(Level.Trace); }
        }

        public override bool IsInfoEnabled
        {
            get { return _log.IsEnabledFor(Level.Info); }
        }

        public override bool IsWarnEnabled
        {
            get { return _log.IsEnabledFor(Level.Warn); }
        }

        public override bool IsErrorEnabled
        {
            get { return _log.IsEnabledFor(Level.Error); }
        }

        public override bool IsFatalEnabled
        {
            get { return _log.IsEnabledFor(Level.Fatal); }
        }
        #endregion
        /// <summary>
        /// 获取日志输出处理委托实例
        /// </summary>
        /// <param name="level">日志输出级别</param>
        /// <param name="message">日志消息</param>
        /// <param name="exception">日志异常</param>
        /// <param name="isData">是否数据日志</param>
        protected override void Write(LogLevel level, object message, Exception exception, bool isData = false)
        {
            if (isData)
            {
                return;
            }
            Level log4NetLevel = GetLevel(level);
            if (message.GetType() != typeof(string))
            {
                message = message.ToJsonString();
            }
            _log.Log(DeclaringType, log4NetLevel, message, exception);
        }
        /// <summary>
        /// 获取日志输出级别
        /// </summary>
        /// <param name="level">日志输出级别枚举</param>
        /// <returns>获取日志输出级别</returns>
        private static Level GetLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.All:
                    return Level.All;
                case LogLevel.Trace:
                    return Level.Trace;
                case LogLevel.Debug:
                    return Level.Debug;
                case LogLevel.Info:
                    return Level.Info;
                case LogLevel.Warn:
                    return Level.Warn;
                case LogLevel.Error:
                    return Level.Error;
                case LogLevel.Fatal:
                    return Level.Fatal;
                case LogLevel.Off:
                    return Level.Off;
                default:
                    return Level.Off;
            }
        }
    }
}