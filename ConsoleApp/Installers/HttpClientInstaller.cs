using DiscordBot.ConsoleUI.Installers.Attributes;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot.ConsoleUI.Installers;
[Installer(true)]
internal class HttpClientInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration config)
    {
        services.AddHttpClient("Api", x => {
            x.BaseAddress = new(config["Api:Endpoint"]!);
            x.DefaultRequestHeaders.Add("x-api-key", config["Api:ApiKey"]);
        });
    }
}
