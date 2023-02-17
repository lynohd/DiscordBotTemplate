using DSharpPlus.CommandsNext;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Shared.Events.Messages.Commands;
public class CommandExecutedMessage : INotification
{
    public CommandExecutionEventArgs Args { get; set; }

    public CommandExecutedMessage(CommandExecutionEventArgs args)
    {
        Args = args;
    }
}
