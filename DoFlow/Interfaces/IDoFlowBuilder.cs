using System;
using System.Threading.Tasks;

namespace DoFlow.Interfaces
{
    /// <summary>
    /// 构建工作流任务
    /// </summary>
    public interface IDoFlowBuilder
    {
        /// <summary>
        /// 开始一个 step
        /// </summary>
        IStepBuilder StartWith(Action action = null);
        void EndWith(Action action);

        Task ThatTask { get; }
    }
}
