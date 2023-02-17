using DiscordBot.ConsoleUI.Installers.Attributes;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System.Reflection;

namespace DiscordBot.ConsoleUI.Installers.Extensions;
public static class InstallerExtension
{
    static ILogger Logger;
    static InstallerExtension()
    {
        Logger = LoggerFactory.Create(x =>
        {
            x.SetMinimumLevel(LogLevel.Debug);
            x.AddConsole();
        }).CreateLogger("ServiceInstaller");
    }

    public static void Install(this IServiceCollection services, IConfiguration config, params Assembly[] assemblies)
    {

        var installers = assemblies
            .SelectMany(x => x.DefinedTypes)
            .Where(IsValidInstaller)
            .Where(HasAttribute<InstallerAttribute>)
            .Where(IsEnabled)
            .Select(Activator.CreateInstance)
            .Cast<IServiceInstaller>();

        //.Where(x => x.IsAssignableTo(typeof(IServiceInstaller)) == true && x.IsInterface == false)

        //.Where(x => x.IsInterface == false)
        foreach (var installer in installers)
        {
            installer?.Install(services, config);
        }
    }


    static bool IsValidInstaller(TypeInfo typeInfo)
    {
        return typeInfo.IsAssignableTo(typeof(IServiceInstaller)) == true && typeInfo.IsInterface == false;
    }

    static bool HasAttribute<TAttribute>(TypeInfo typeInfo) where TAttribute : Attribute
    {
        var hasAttribute = typeInfo.GetCustomAttribute<TAttribute>() != null;

        if (hasAttribute is false)
        {
            Logger.LogWarning("{Service} does not have {Attribute} and cannot be resolved", typeInfo, nameof(InstallerAttribute));
        }

        //Logger.LogInformation("{Installer} was found", typeInfo);
        return hasAttribute;
    }

    static bool IsEnabled(TypeInfo info)
    {
        var enabled = info.GetCustomAttribute<InstallerAttribute>().Enabled;
        Logger.LogInformation("{Service} has been registered", info);
        return enabled;
    }
}
