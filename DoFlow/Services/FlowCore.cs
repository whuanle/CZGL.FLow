using DoFlow.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;

namespace DoFlow.Services
{
    public static class FlowCore
    {
        private static Dictionary<int, FlowEngine> flowEngines = new Dictionary<int, FlowEngine>();

        // 读写锁
        private static ReaderWriterLockSlim readerWriterLockSlim = new ReaderWriterLockSlim();

        /// <summary>
        /// 注册工作流
        /// </summary>
        /// <param name="flow"></param>
        public static bool RegisterWorkflow(IDoFlow flow)
        {
            try
            {
                readerWriterLockSlim.EnterReadLock();
                if (flowEngines.ContainsKey(flow.Id))
                    return false;
                flowEngines.Add(flow.Id, new FlowEngine(flow));
                return true;
            }
            finally
            {
                readerWriterLockSlim.ExitReadLock();
            }
        }

        /// <summary>
        /// 注册工作流
        /// </summary>
        /// <param name="flow"></param>
        public static bool RegisterWorkflow<TDoFlow>()
        {

            Type type = typeof(TDoFlow);
            IDoFlow flow = (IDoFlow)Activator.CreateInstance(type);
            try
            {
                readerWriterLockSlim.EnterReadLock();
                if (flowEngines.ContainsKey(flow.Id))
                    return false;
                flowEngines.Add(flow.Id, new FlowEngine(flow));
                return true;
            }
            finally
            {
                readerWriterLockSlim.ExitReadLock();
            }
        }

        /// <summary>
        /// 要启动的工作流
        /// </summary>
        /// <param name="id"></param>
        public static bool Start(int id)
        {
            FlowEngine engine;
            // 读写锁
            try
            {
                readerWriterLockSlim.EnterUpgradeableReadLock();

                if (!flowEngines.ContainsKey(id))
                    return default;
                try
                {
                    readerWriterLockSlim.EnterWriteLock();
                    engine = flowEngines[id];
                }
                catch { return default; }
                finally
                {
                    readerWriterLockSlim.ExitWriteLock();
                }
            }
            catch { return default; }
            finally
            {
                readerWriterLockSlim.ExitUpgradeableReadLock();
            }

            engine.Start();
            return true;
        }

    }
}
