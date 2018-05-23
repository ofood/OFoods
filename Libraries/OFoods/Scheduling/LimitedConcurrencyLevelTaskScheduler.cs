using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OFoods.Scheduling
{
    public class LimitedConcurrencyLevelTaskScheduler : TaskScheduler
    {
        /// <summary>
        /// 当前线程是否正在处理工作项目
        /// </summary>
        [ThreadStatic]
        private static bool _currentThreadIsProcessingItems;
        /// <summary>
        /// 要执行的任务列表
        /// </summary>
        private readonly LinkedList<Task> _tasks = new LinkedList<Task>(); // protected by lock(_tasks)
        /// <summary>
        /// 此调度程序允许的最大并发级别
        /// </summary>
        private readonly int _maxDegreeOfParallelism;
        /// <summary>
        /// 调度器是否正在处理工作项目
        /// </summary>
        private int _delegatesQueuedOrRunning = 0; // protected by lock(_tasks)

        /// <summary>
        /// 使用指定的并行度初始化LimitedConcurrencyLevelTaskScheduler类的一个实例
        /// </summary>
        /// <param name="maxDegreeOfParallelism">
        /// 这个调度程序提供的最大并行度
        /// </param>
        public LimitedConcurrencyLevelTaskScheduler(int maxDegreeOfParallelism)
        {
            if (maxDegreeOfParallelism < 1) throw new ArgumentOutOfRangeException("maxDegreeOfParallelism");
            _maxDegreeOfParallelism = maxDegreeOfParallelism;
        }

        /// <summary>
        /// 将任务排入调度程序
        /// </summary>
        /// <param name="task">
        /// 要排队的任务
        /// </param>
        protected sealed override void QueueTask(Task task)
        {
            // 将任务添加到要处理的任务列表。 如果没有足够的代表排队或正在运行以处理任务，则安排另一个代表
            lock (_tasks)
            {
                _tasks.AddLast(task);
                if (_delegatesQueuedOrRunning < _maxDegreeOfParallelism)
                {
                    ++_delegatesQueuedOrRunning;
                    NotifyThreadPoolOfPendingWork();
                }
            }
        }

        /// <summary>
        /// 通知ThreadPool这个调度程序有工作要执行
        /// </summary>
        private void NotifyThreadPoolOfPendingWork()
        {
            ThreadPool.UnsafeQueueUserWorkItem(_ =>
            {
                // Note that the current thread is now processing work items.
                // This is necessary to enable inlining of tasks into this thread.
                _currentThreadIsProcessingItems = true;
                try
                {
                    // Process all available items in the queue.
                    while (true)
                    {
                        Task item;
                        lock (_tasks)
                        {
                            // When there are no more items to be processed,
                            // note that we're done processing, and get out.
                            if (_tasks.Count == 0)
                            {
                                --_delegatesQueuedOrRunning;
                                break;
                            }

                            // Get the next item from the queue
                            item = _tasks.First.Value;
                            _tasks.RemoveFirst();
                        }

                        // Execute the task we pulled out of the queue
                        base.TryExecuteTask(item);
                    }
                }
                // We're done processing items on the current thread
                finally { _currentThreadIsProcessingItems = false; }
            }, null);
        }

        /// <summary>
        /// 尝试在当前线程上执行指定的任务
        /// </summary>
        /// <param name="task">要执行的任务</param>
        /// <param name="taskWasPreviouslyQueued"></param>
        /// <returns>是否可以在当前线程上执行任务</returns>
        protected sealed override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            // If this thread isn't already processing a task, we don't support inlining
            if (!_currentThreadIsProcessingItems) return false;

            // If the task was previously queued, remove it from the queue
            if (taskWasPreviouslyQueued) TryDequeue(task);

            // Try to run the task.
            return base.TryExecuteTask(task);
        }

        /// <summary>
        /// 尝试从调度程序中删除先前计划的任务
        /// </summary>
        /// <param name="task">要删除的任务</param>
        /// <returns>是否可以找到并删除任务</returns>
        protected sealed override bool TryDequeue(Task task)
        {
            lock (_tasks) return _tasks.Remove(task);
        }

        /// <summary>
        /// 获取此调度程序支持的最大并发级别
        /// </summary>
        public sealed override int MaximumConcurrencyLevel { get { return _maxDegreeOfParallelism; } }

        /// <summary>获取当前在此调度程序上调度的任务的枚举</summary>
        /// <returns>当前计划的任务的枚举</returns>
        protected sealed override IEnumerable<Task> GetScheduledTasks()
        {
            bool lockTaken = false;
            try
            {
                Monitor.TryEnter(_tasks, ref lockTaken);
                if (lockTaken) return _tasks.ToArray();
                else throw new NotSupportedException();
            }
            finally
            {
                if (lockTaken) Monitor.Exit(_tasks);
            }
        }
    }
}
