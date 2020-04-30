using DoFlow.Interfaces;
using System;
using System.Threading.Tasks;

namespace DoFlow.Services
{
    public class DoFlowBuilder : IDoFlowBuilder
    {
        private Task _task;
        public Task ThatTask => _task;

        public void EndWith(Action action)
        {
            _task.Start();
        }

        public IStepBuilder StartWith(Action action = null)
        {
            if (action is null)
                _task = new Task(() => { });
            else _task = new Task(action);

            IStepBuilder _stepBuilder = DependencyInjectionService.GetService<IStepBuilder>();
            ((StepBuilder)_stepBuilder).SetTask(_task);
            return _stepBuilder;
        }
    }
}
