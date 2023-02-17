using DiscordBot.ConsoleUI.Services;
using DiscordBot.Shared.Events.Messages.Gateway;

using DSharpPlus.Entities;

using MediatR;

using Microsoft.Extensions.Logging;

namespace DiscordBot.ConsoleUI.Handlers;
public class ReadyMessageHandler : INotificationHandler<ReadyMessage>
{
    private readonly IStatusMessageProvider _statusMessage;
    private readonly ILogger<ReadyMessageHandler> _logger;

    public ReadyMessageHandler(IStatusMessageProvider statusMessage, ILogger<ReadyMessageHandler> logger)
    {
        _statusMessage = statusMessage;
        _logger = logger;
    }


    public async Task Handle(ReadyMessage notification, CancellationToken cancellationToken)
    {
        var activity = new DiscordActivity(_statusMessage.GetStatusMessage(), ActivityType.Playing);
        await notification.Client.UpdateStatusAsync(activity);
    }
}
