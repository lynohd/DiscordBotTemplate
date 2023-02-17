
using DiscordBot.ConsoleUI.Commands;
using DiscordBot.ConsoleUI.Installers.Attributes;
using DiscordBot.Shared.Events.Messages;
using DiscordBot.Shared.Services;

using DSharpPlus;
using DSharpPlus.CommandsNext;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace DiscordBot.ConsoleUI.Installers;
[Installer(true)]
public class DiscordInstaller : IServiceInstaller
{
	

    public void Install(IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IDiscordService, DiscordService>();
        services.AddSingleton<CommandRegister>(x => new CommandRegister() { Assembly = Assembly.GetAssembly(typeof(AdminCommands)) });

        services.AddOptions<DiscordConfiguration>().Configure(x =>
        {
            x.Token = config["Discord:Token"];
            x.Intents = DiscordIntents.All;
        });
        services.AddOptions<CommandsNextConfiguration>().Configure(x =>
        {
            var scope = services.BuildServiceProvider().CreateScope();
            x.PrefixResolver = (msg) =>
            {
                var mediator = scope.ServiceProvider.GetService<IMediator>()!;
                return mediator.Send<int>(new CustomPrefixResolverMessage(msg));

            };
            x.Services = scope.ServiceProvider;
        });
        services.AddHostedService<DiscordRunner>();
    }
}
