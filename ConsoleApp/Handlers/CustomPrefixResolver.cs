using DiscordBot.DataAccess.DbContexts;
using DiscordBot.Shared.Events.Messages;

using DSharpPlus.CommandsNext;

using MediatR;
namespace DiscordBot.ConsoleUI.Handlers;


public class CustomPrefixResolver : IRequestHandler<CustomPrefixResolverMessage, int>
{
    private readonly DiscordContext _db;

    public CustomPrefixResolver(DiscordContext db)
    {
        _db = db;
    }

    public Task<int> Handle(CustomPrefixResolverMessage request, CancellationToken cancellationToken)
    {
        var guild = request.Message.Channel.Guild;

        var prefix = _db.Guilds.Where(x => x.GuildId == guild.Id).First().Prefix;

        return Task.FromResult(request.Message.GetStringPrefixLength(prefix));
    }
}
