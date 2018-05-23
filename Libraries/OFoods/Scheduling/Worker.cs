using System;
using System.Threading;
using OFoods.Dependency;
using OFoods.Logging;

namespace OFoods.Scheduling
{
    /// <summary>
    /// 表示将重复执行特定方法的后台工作人员
    /// </summary>
    public class Worker
    {
        private readonly object _lockObject = new object();
        private readonly string _actionName;
        private readonly Action _action;
        private readonly ILogger _logger;
        private Status _status;

        /// <summary>
        /// 返回当前工作的操作名称
        /// </summary>
        public string ActionName
        {
            get { return _actionName; }
        }

        /// <summary>
        /// 使用指定的操作初始化新的工作人员
        /// </summary>
        /// <param name="actionName">行为名称</param>
        /// <param name="action">The action to run by the worker.</param>
        public Worker(string actionName, Action action)
        {
            _actionName = actionName;
            _action = action;
            _status = Status.Initial;
            _logger = IocResolver.Instance.Resolve<ILoggerFactory>().Create(GetType().FullName);
        }

        /// <summary>
        /// 如果没有运行，启动worker
        /// </summary>
        public Worker Start()
        {
            lock (_lockObject)
            {
                if (_status == Status.Running) return this;

                _status = Status.Running;
                new Thread(Loop)
                {
                    Name = string.Format("{0}.Worker", _actionName),
                    IsBackground = true
                }.Start(this);

                return this;
            }
        }
        /// <summary>
        /// 请求停止Worker
        /// </summary>
        public Worker Stop()
        {
            lock (_lockObject)
            {
                if (_status == Status.StopRequested) return this;

                _status = Status.StopRequested;

                return this;
            }
        }

        private void Loop(object data)
        {
            var worker = (Worker)data;

            while (worker._status == Status.Running)
            {
                try
                {
                    _action();
                }
                catch (ThreadAbortException)
                {
                    _logger.Info("Worker thread caught ThreadAbortException, try to resetting, actionName:{0}", _actionName);
                    Thread.ResetAbort();
                    _logger.Info("Worker thread ThreadAbortException resetted, actionName:{0}", _actionName);
                }
                catch (Exception ex)
                {
                    _logger.Error(string.Format("Worker thread has exception, actionName:{0}", _actionName), ex);
                }
            }
        }

        enum Status
        {
            Initial,
            Running,
            StopRequested
        }
    }
}
