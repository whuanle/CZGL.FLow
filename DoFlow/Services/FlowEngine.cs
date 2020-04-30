using DoFlow.Interfaces;

namespace DoFlow.Services
{
    /// <summary>
    /// 工作流引擎
    /// </summary>
    public class FlowEngine
    {
        private readonly IDoFlow _flow;
        public FlowEngine(IDoFlow flow)
        {
            _flow = flow;
        }

        /// <summary>
        /// 开始一个工作流
        /// </summary>
        public void Start()
        {
            IDoFlowBuilder builder = DependencyInjectionService.GetService<IDoFlowBuilder>();
            _flow.Build(builder).ThatTask.Start();
        }
    }
}
