using Gateway;
using Microsoft.AspNetCore.Builder;
using Ocelot.Cache;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot().AddConsul<MyConsulServiceBuilder>().AddPolly();
//builder.Services.AddSingleton<IOcelotCache<CachedResponse>,OcelotCache>();
var app = builder.Build();
await app.UseOcelot();
app.Run();
