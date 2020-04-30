using DoFlow.Interfaces;
using DoFlow.Services;

namespace DoFlow.Extensions
{
    public static class InitExtension
    {
        private static bool IsInit = false;
        public static void StartInitExtension()
        {
            if (IsInit) return;
            IsInit = true;
            DependencyInjectionService.AddService<IStepBuilder, StepBuilder>();
            DependencyInjectionService.AddService<IDoFlowBuilder, DoFlowBuilder>();
            DependencyInjectionService.AddService<IStepParallel, StepParallelWhenAll>();
            DependencyInjectionService.AddService<IStepParallelAny, StepParallelWhenAny>();
        }
    }
}
