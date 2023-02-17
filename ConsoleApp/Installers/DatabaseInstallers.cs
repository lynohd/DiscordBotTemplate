using DiscordBot.ConsoleUI.Installers.Attributes;
using DiscordBot.DataAccess.DbContexts;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot.ConsoleUI.Installers;

[Installer(true)]
public class DatabaseInstallers : IServiceInstaller
{
    

    public void Install(IServiceCollection services, IConfiguration config)
    {
        
        services.AddDbContext<DiscordContext>(optionsAction: options => {
            options.UseSqlServer(config.GetConnectionString("Discord"));
        });
    }
}
