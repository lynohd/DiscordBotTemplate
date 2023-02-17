using DiscordBot.Shared.Events.Messages.Commands;
using DiscordBot.Shared.Events.Messages.Gateway;

using DSharpPlus;
using DSharpPlus.CommandsNext;

using MediatR;

using Microsoft.Extensions.Options;

namespace DiscordBot.Shared.Services;
public class DiscordService : IDiscordService, IDisposable
{
    DiscordClient _client;
    CommandsNextExtension _commands;
    IPublisher _publisher;
    CommandRegister _commandRegister;

    public DiscordClient DiscordClient => _client;

    public DiscordService(
        IOptions<DiscordConfiguration> discordConfig,
        IOptions<CommandsNextConfiguration> commandsConfig,
        IPublisher publisher, CommandRegister commandRegister)
        : this(discordConfig.Value, commandsConfig.Value, publisher, commandRegister)
    {
    }


    private DiscordService(DiscordConfiguration discordConfiguration, CommandsNextConfiguration commandsConfig, IPublisher publisher, CommandRegister commandRegister)
    {
        _client = new DiscordClient(discordConfiguration);
        _commands = _client.UseCommandsNext(commandsConfig);
        _commands.RegisterCommands(commandRegister.Assembly);
        _publisher = publisher;
        RegisterEventHandlers();
    }


#pragma warning disable CS4014
    private void RegisterEventHandlers()
    {
        _client.Ready += async (c, e) => await _publisher.Publish(new ReadyMessage(c, e));
        _client.ClientErrored += async (c, e) => await _publisher.Publish(new ClientErrorMessage(c, e));
        _client.GuildAvailable += async (c, e) => await _publisher.Publish(new GuildAvailableMessage(e.Guild));
        _commands.CommandExecuted += async (c, e) => await _publisher.Publish(new CommandExecutedMessage(e));
        _commands.CommandErrored += async (c, e) => await _publisher.Publish(new CommandErroredMessage(e));
    }
#pragma warning restore CS4014 





    public async Task StartAsync() => await _client.ConnectAsync();
    public async Task StopAsync() => await _client.DisconnectAsync();

    public void Dispose()
    {
        _client.Ready -= async (c, e) => await _publisher.Publish(new ReadyMessage(c, e));
        _client.ClientErrored -= async (c, e) => await _publisher.Publish(new ClientErrorMessage(c, e));
        _client.GuildAvailable -= async (c, e) => await _publisher.Publish(new GuildAvailableMessage(e.Guild));
        _commands.CommandExecuted -= async (c, e) => await _publisher.Publish(new CommandExecutedMessage(e));
        _commands.CommandErrored -= async (c, e) => await _publisher.Publish(new CommandErroredMessage(e));

        _commands?.Dispose();
        _client?.Dispose();
    }
}
