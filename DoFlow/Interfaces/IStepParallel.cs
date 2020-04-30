using System;

namespace DoFlow.Interfaces
{
    /// <summary>
    /// 并行任务
    ///  <para>默认情况下，只有这个节点的所有并行任务都完成后，这个节点才算完成</para>
    /// </summary>
    public interface IStepParallel
    {
        /// <summary>
        /// 一个并行任务
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        IStepParallel Do(Action action);

        /// <summary>
        /// 开始一个分支
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        IStepBuilder StartWith(Action action = null);

        /// <summary>
        /// 必须使用此方法结束一个并行任务
        /// </summary>
        void EndParallel();
    }

    /// <summary>
    /// 并行任务
    /// <para>任意一个任务完成后，就可以跳转到下一个 step</para>
    /// </summary>
    public interface IStepParallelAny : IStepParallel
    {

    }
}
