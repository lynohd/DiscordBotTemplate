using DSharpPlus;
using DSharpPlus.EventArgs;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Shared.Events.Messages;
public abstract class BaseDiscordMessage : INotification
{
    public DiscordEventArgs EventArgs { get; set; }
    public DiscordClient Client { get; set; }

    public BaseDiscordMessage(DiscordClient client, DiscordEventArgs eventArgs)
    {
        EventArgs = eventArgs;
        Client = client;
    }
}
