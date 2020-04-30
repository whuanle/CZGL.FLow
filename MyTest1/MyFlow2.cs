using DoFlow.Interfaces;
using System;
using System.Threading.Tasks;

namespace MyTest1
{

    /// <summary>
    /// 并行节点 Parallel 使用方法
    /// </summary>
    public class MyFlow2 : IDoFlow
    {
        public int Id => 2;
        public string Name => "test";
        public int Version => 1;

        public IDoFlowBuilder Build(IDoFlowBuilder builder)
        {
            builder.StartWith()
                .Parallel(steps =>
                {
                    // 每个并行任务也可以设计后面继续执行其它任务
                    steps.Do(() =>
                    {
                        Console.WriteLine("并行1");
                    }).Do(() =>
                    {
                        Console.WriteLine("并行2");
                    });
                    steps.Do(() =>
                    {
                        Console.WriteLine("并行3");
                    });

                    // 并行任务设计完成后，必须调用此方法
                    // 此方法必须放在所有并行任务 .Do() 的最后
                    steps.EndParallel();

                    // 如果 .Do() 在 EndParallel() 后，那么不会等待此任务
                    steps.Do(() => { Console.WriteLine("并行异步"); });

                    // 开启新的分支
                    steps.StartWith()
                    .Then(() =>
                    {
                        Console.WriteLine("新的分支" + Task.CurrentId);
                    }).Then(() => { Console.WriteLine("分支2.0" + Task.CurrentId); });

                }, false)
                .Then(() =>
                {
                    Console.WriteLine("11111111111111111 ");
                });

            return builder;
        }
    }
}
