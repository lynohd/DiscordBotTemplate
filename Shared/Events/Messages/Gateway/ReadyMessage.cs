using DSharpPlus;
using DSharpPlus.EventArgs;

using MediatR;

namespace DiscordBot.Shared.Events.Messages.Gateway;
public class ReadyMessage : INotification
{
    public ReadyMessage(DiscordClient client, ReadyEventArgs eventArgs)
    {
        Client = client;
        EventArgs = eventArgs;
    }

    public DiscordClient Client { get; set; }
    public ReadyEventArgs EventArgs { get; set; }
}