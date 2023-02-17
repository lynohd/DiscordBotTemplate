using DSharpPlus.CommandsNext;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Shared.Events.Messages.Commands;
public class CommandErroredMessage : INotification
{
    public CommandErrorEventArgs Args { get; set; }

    public CommandErroredMessage(CommandErrorEventArgs args)
    {
        Args = args;
    }
}
