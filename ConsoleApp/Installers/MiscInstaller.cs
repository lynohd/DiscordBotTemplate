using DiscordBot.ConsoleUI.Installers.Attributes;
using DiscordBot.ConsoleUI.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot.ConsoleUI.Installers;
[Installer(true)]
internal class MiscInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration config)
    {
        services.AddTransient<IStatusMessageProvider, StatusMessageProvider>();
    }
}
