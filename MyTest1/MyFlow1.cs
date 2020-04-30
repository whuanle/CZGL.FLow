using DoFlow.Interfaces;
using System;

namespace MyTest1
{
    /// <summary>
    /// 普通节点 Then 使用方法
    /// </summary>
    public class MyFlow1 : IDoFlow
    {
        public int Id => 1;
        public string Name => "test";
        public int Version => 1;

        public IDoFlowBuilder Build(IDoFlowBuilder builder)
        {
            builder.StartWith(() =>
            {
                Console.WriteLine("工作流开始");
            }).Then(() =>
            {
                Console.WriteLine("下一个节点");
            }).Then(() =>
             {
                 Console.WriteLine("最后一个节点");
             });
            return builder;
        }
    }
}
