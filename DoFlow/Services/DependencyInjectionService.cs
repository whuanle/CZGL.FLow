using DoFlow.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DoFlow.Services
{
    /// <summary>
    /// 依赖注入服务
    /// </summary>
    public static class DependencyInjectionService
    {
        private static IServiceCollection _servicesList;
        private static IServiceProvider _services;
        static DependencyInjectionService()
        {
            IServiceCollection services = new ServiceCollection();
            _servicesList = services;
            // 注入引擎需要的服务
            InitExtension.StartInitExtension();
            var serviceProvider = services.BuildServiceProvider();
            _services = serviceProvider;
        }

        /// <summary>
        /// 添加一个注入到容器服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        public static void AddService<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _servicesList.AddTransient<TService, TImplementation>();
        }

        /// <summary>
        /// 获取需要的服务
        /// </summary>
        /// <typeparam name="TIResult"></typeparam>
        /// <returns></returns>
        public static TIResult GetService<TIResult>()
        {
            TIResult Tservice = _services.GetService<TIResult>();
            return Tservice;
        }
    }
}
