using DoFlow.Interfaces;
using System;

namespace MyTest1
{
    public class MyFlow3 : IDoFlow
    {
        public int Id => 3;

        public string Name => "666";

        public int Version => 1;

        public IDoFlowBuilder Build(IDoFlowBuilder builder)
        {
            builder.StartWith()
                .Then(() =>
                {
                    Console.WriteLine("任务开始，设置定时器2秒");
                })
                .Schedule(() =>
                {
                    Console.WriteLine("我被执行了");
                }, TimeSpan.FromSeconds(2))
                .Then(() =>
                {
                    Console.WriteLine("另一个任务我不管他");
                });
            return builder;
        }
    }
}
