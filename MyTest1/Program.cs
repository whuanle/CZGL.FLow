using DoFlow.Services;
using System;

namespace MyTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            FlowCore.RegisterWorkflow<MyFlow1>();
            // FlowCore.RegisterWorkflow(new MyFlow1());
            FlowCore.RegisterWorkflow<MyFlow2>();
            FlowCore.RegisterWorkflow<MyFlow3>();

            FlowCore.Start(2);
            Console.ReadKey();
        }
    }
}
