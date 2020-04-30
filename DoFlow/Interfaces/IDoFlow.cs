namespace DoFlow.Interfaces
{

    /// <summary>
    /// 工作流
    /// <para>无参数传递</para>
    /// </summary>
    public interface IDoFlow
    {
        /// <summary>
        /// 全局唯一标识
        /// </summary>
        int Id { get; }

        /// <summary>
        /// 标识此工作流的名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 标识此工作流的版本
        /// </summary>
        int Version { get; }

        IDoFlowBuilder Build(IDoFlowBuilder builder);
    }
}
