using Consul;
using Ocelot.Logging;
using Ocelot.Provider.Consul.Interfaces;
using Ocelot.Provider.Consul;
using Microsoft.AspNetCore.Http;

namespace Gateway
{
    public class MyConsulServiceBuilder : DefaultConsulServiceBuilder
    {
        public MyConsulServiceBuilder(IHttpContextAccessor contextAccessor, IConsulClientFactory clientFactory, IOcelotLoggerFactory loggerFactory)
            : base(contextAccessor, clientFactory, loggerFactory) { }

        // I want to use the agent service IP address as the downstream hostname
        protected override string GetDownstreamHost(ServiceEntry entry, Node node)
            => entry.Service.Address;
    }
}
