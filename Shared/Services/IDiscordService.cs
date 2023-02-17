using DSharpPlus;

namespace DiscordBot.Shared.Services;
public interface IDiscordService : IDisposable
{
    DiscordClient DiscordClient { get; }

    Task StartAsync();
    Task StopAsync();
}