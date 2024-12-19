using Consul.AspNetCore;
using Consul;

namespace MyProject1
{
    public static class ConsulCollectionExtension
    {
        public static IServiceCollection AddConsulService(this IServiceCollection services, IConfiguration configuration)
        {
            var consulOptions = configuration.GetSection("Consul").Get<ConsulOptions>();
            // 通过consul提供的注入方式注册consulClient
            services.AddConsul(options => options.Address = new Uri($"http://{consulOptions.ConsulIP}:{consulOptions.ConsulPort}"));
            var addres = $"http://{consulOptions.IP}:{consulOptions.Port}/health/health";//健康检查地址
            // 通过consul提供的注入方式进行服务注册
            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册
                Interval = TimeSpan.FromSeconds(5),//健康检查时间间隔，或者称为心跳间隔
                HTTP = addres,//健康检查地址
                Timeout = TimeSpan.FromSeconds(10)
            };

            // Register service with consul
            services.AddConsulServiceRegistration(options =>
            {
                options.Checks = new[] { httpCheck };
                options.ID = Guid.NewGuid().ToString();
                options.Name = consulOptions.ServiceName;
                options.Address = consulOptions.IP;
                options.Port = consulOptions.Port;
                options.Meta = new Dictionary<string, string>() { { "Weight", consulOptions.Weight.HasValue ? consulOptions.Weight.Value.ToString() : "1" } };
                options.Tags = new[] { $"urlprefix-/{consulOptions.ServiceName}" }; //添加
            });

            return services;
        }
        /// <summary>
        /// 配置Consul检查服务
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseConsulCheckService(this IApplicationBuilder app)
        {
            app.Map("/health", app =>
            {
                app.Run(async context =>
                {
                    await Task.Run(() => context.Response.StatusCode = 200);
                });
            });

            return app;
        }
    }
}
