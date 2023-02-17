using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Shared.Events.Messages.Gateway;
public class GuildAvailableMessage : INotification
{
    public DiscordGuild Guild { get; set; }
    public GuildAvailableMessage(DiscordGuild args)
    {
        Guild = args;
    }
}
