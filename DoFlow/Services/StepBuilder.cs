using DoFlow.Interfaces;
using System;
using System.Threading.Tasks;

namespace DoFlow.Services
{

    /// <summary>
    /// 节点工作引擎
    /// </summary>
    public class StepBuilder : IStepBuilder
    {
        private Task _task;

        /// <summary>
        /// 延迟执行
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public IStepBuilder Delay(TimeSpan time)
        {
            Task.Delay(time).Wait();
            return this;
        }

        /// <summary>
        /// 并行 step
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public IStepBuilder Parallel(Action<IStepParallel> action, bool anyAwait = false)
        {
            IStepParallel parallel = anyAwait ? DependencyInjectionService.GetService<IStepParallelAny>() : DependencyInjectionService.GetService<IStepParallel>();
            Task task = new Task(() =>
            {
                action.Invoke(parallel);
            });

            _task.ConfigureAwait(false).GetAwaiter().OnCompleted(() =>
            {
                task.Start();
            });
            _task = task;
            return this;
        }

        /// <summary>
        /// 计划任务
        /// </summary>
        /// <param name="action"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public IStepBuilder Schedule(Action action, TimeSpan time)
        {
            Task.Factory.StartNew(() =>
            {
                Task.Delay(time).Wait();
                action.Invoke();
            });
            return this;
        }

        /// <summary>
        /// 普通 step
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public IStepBuilder Then(Action action)
        {
            Task task = new Task(action);
            _task.ConfigureAwait(false).GetAwaiter().OnCompleted(() =>
            {
                task.Start();
                task.Wait();
            });
            _task = task;
            return this;
        }

        public void SetTask(Task task)
        {
            _task = task;
        }
    }
}
