using System;

namespace DoFlow.Interfaces
{
    public interface IStepBuilder
    {

        /// <summary>
        /// 普通节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <returns></returns>
        IStepBuilder Then(Action action);

        /// <summary>
        /// 多个节点
        /// <para>默认下，需要等待所有的任务完成，这个step才算完成</para>
        /// </summary>
        /// <param name="action"></param>
        /// <param name="anyWait">任意一个任务完成即可跳转到下一个step</param>
        /// <returns></returns>
        IStepBuilder Parallel(Action<IStepParallel> action, bool anyWait = false);

        /// <summary>
        /// 节点将在某个时间间隔后执行
        /// <para>异步，不会阻塞当前工作流的运行，计划任务将在一段时间后触发</para>
        /// </summary>
        /// <returns></returns>
        IStepBuilder Schedule(Action action, TimeSpan time);

        /// <summary>
        /// 阻塞一段时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        IStepBuilder Delay(TimeSpan time);
    }
}
