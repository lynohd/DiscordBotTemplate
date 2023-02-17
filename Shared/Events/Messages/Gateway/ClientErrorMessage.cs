using DSharpPlus;
using DSharpPlus.EventArgs;

using MediatR;

namespace DiscordBot.Shared.Events.Messages.Gateway;
internal class ClientErrorMessage : INotification
{
    private DiscordClient _c;
    private ClientErrorEventArgs _e;

    public ClientErrorMessage(DiscordClient client, ClientErrorEventArgs eventArgs)
    {
        _c = client;
        _e = eventArgs;
    }
}