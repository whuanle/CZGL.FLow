using DoFlow.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoFlow.Services
{
    /// <summary>
    /// 第一层所有任务结束后才能跳转下一个 step
    /// </summary>
    public class StepParallelWhenAll : IStepParallel
    {
        private Task _task;
        private readonly List<Task> _tasks = new List<Task>();
        public StepParallelWhenAll()
        {
            _task = new Task(() => { },TaskCreationOptions.AttachedToParent);
        }
        public IStepParallel Do(Action action)
        {
            _tasks.Add(Task.Run(action));
            return this;
        }

        public void EndParallel()
        {
            _task.ConfigureAwait(false).GetAwaiter().OnCompleted(() =>
            {
                Task.WhenAll(_tasks).Wait();
            });
        }

        public IStepBuilder StartWith(Action action = null)
        {
            Task task =
                action is null ? new Task(() => { })
                : new Task(action);
            var _stepBuilder = DependencyInjectionService.GetService<IStepBuilder>();
            _task.ConfigureAwait(false).GetAwaiter().OnCompleted(() => { task.Start(); });

            return _stepBuilder;
        }
    }

    /// <summary>
    /// 完成任意一个任务即可跳转到下一个 step
    /// </summary>
    public class StepParallelWhenAny : IStepParallelAny
    {
        private Task _task;
        private readonly List<Task> _tasks = new List<Task>();
        public StepParallelWhenAny()
        {
            _task = Task.Run(() => { });
        }
        public IStepParallel Do(Action action)
        {
            _tasks.Add(Task.Run(action));
            return this;
        }

        public void EndParallel()
        {
            _task.ConfigureAwait(false).GetAwaiter().OnCompleted(() =>
            {
                Task.WhenAny(_tasks).Wait();
            });
        }

        public IStepBuilder StartWith(Action action = null)
        {
            Task task =
                action is null ? new Task(() => { })
                : new Task(action);
            var _stepBuilder = DependencyInjectionService.GetService<IStepBuilder>();
            _task.ConfigureAwait(false).GetAwaiter().OnCompleted(() => { task.Start(); });

            return _stepBuilder;
        }
    }
}
