using DiscordBot.ConsoleUI.Installers.Attributes;
using DiscordBot.Shared.Services;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot.ConsoleUI.Installers;
[Installer(true)]
internal class MediatRInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration config)
    {
        services.AddMediatR(typeof(Program), typeof(DiscordService));
    }
}
