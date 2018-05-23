using System;

namespace OFoods.Scheduling
{
    public interface IScheduleService
    {
        /// <summary>
        /// 启动任务调度
        /// </summary>
        /// <param name="name">任务名称</param>
        /// <param name="action"></param>
        /// <param name="dueTime"></param>
        /// <param name="period"></param>
        void StartTask(string name, Action action, int dueTime, int period);
        /// <summary>
        /// 停止任务调度
        /// </summary>
        /// <param name="name">任务名称</param>
        void StopTask(string name);
    }
}
