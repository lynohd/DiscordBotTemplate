using DiscordBot.Shared.Events.Messages.Commands;

using MediatR;

using Microsoft.Extensions.Logging;

namespace DiscordBot.ConsoleUI.Handlers;
public class DiscordCommandHandlers : INotificationHandler<CommandExecutedMessage>
{
    ILogger<DiscordCommandHandlers> _logger;
    public DiscordCommandHandlers(ILogger<DiscordCommandHandlers> logger)
    {
        _logger = logger;
    }

    public Task Handle(CommandExecutedMessage notification, CancellationToken cancellationToken)
    {
        var args = notification.Args;
        var ctx = args.Context;
        var command = args.Command;
        var user = ctx.User;

        _logger.LogInformation("{user} executed {command} with arguments: {args}", user, command, ctx.RawArgumentString);
        return Task.CompletedTask;
    }
}
