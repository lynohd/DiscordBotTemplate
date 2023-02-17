
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using DiscordBot.ConsoleUI.Installers.Extensions;

var assembly = Assembly.GetExecutingAssembly();

var host = Host
    .CreateDefaultBuilder()
    .ConfigureDefaults(args)
    //.ConfigureHostConfiguration(x => x.AddJsonFile("appconfig.json", false).Build())
    .ConfigureHostConfiguration(x =>
    {
        //x.AddJsonFile("appconfig.json", false).Build();
        x.AddUserSecrets(assembly).Build();
    })
    .ConfigureServices((ctx, services) => services.Install(ctx.Configuration, assembly))
    .Build();

/*
{

    services.Install(ctx.Configuration, Assembly.GetExecutingAssembly());


    services.AddDbContext<DiscordContext>(optionsAction: options =>
    {
        options.UseSqlServer(ctx.Configuration.GetConnectionString("Discord"));
    });

    services.AddHttpClient("Api", x =>
    {
        x.BaseAddress = new(ctx.Configuration["Api:Endpoint"]!);
        x.DefaultRequestHeaders.Add("x-api-key", ctx.Configuration["Api:ApiKey"]);
    });

    services.AddMediatR(typeof(Program), typeof(DiscordService));
    services.AddTransient<IStatusMessageProvider, StatusMessageProvider>();
    services.AddHostedService<DiscordRunner>();
})
    .ConfigureDiscord()

*/

await host.StartAsync();
await Task.Delay(-1);
