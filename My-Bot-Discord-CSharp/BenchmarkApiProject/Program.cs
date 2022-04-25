// See https://aka.ms/new-console-template for more information


using NBomber.CSharp;
using NBomber.Plugins.Network.Ping;

var step = Step.Create("fetch_html_page",
                                   clientFactory: HttpClientFactory.Create(),
                                   execute: context =>
                                   {
                                       var request = Http.CreateRequest("GET", "https://localhost:7136/api/Ligne/all")
                                                         .WithHeader("Accept", "text/html");

                                       return Http.Send(request, context);
                                   });

var scenario = ScenarioBuilder

    .CreateScenario("simple_http", step)
    .WithWarmUpDuration(TimeSpan.FromSeconds(10))
    .WithLoadSimulations(
        Simulation.InjectPerSec(rate: 500, during: TimeSpan.FromSeconds(100))
    );

// creates ping plugin that brings additional reporting data
var pingPluginConfig = PingPluginConfig.CreateDefault(new[] { "nbomber.com" });
var pingPlugin = new PingPlugin(pingPluginConfig);

NBomberRunner
    .RegisterScenarios(scenario)
    .WithWorkerPlugins(pingPlugin)
    .Run();


Console.ReadLine();