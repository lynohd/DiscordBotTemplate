using DSharpPlus.Entities;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Shared.Events.Messages;
public class CustomPrefixResolverMessage : IRequest<int>
{

    public CustomPrefixResolverMessage(DiscordMessage message)
    {
        Message = message;
    }

    public DiscordMessage Message { get; }
}
