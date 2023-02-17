using DiscordBot.DataAccess.DbContexts;
using DiscordBot.DataAccess.Models;
using DiscordBot.Shared.Events.Messages.Gateway;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiscordBot.ConsoleUI.Handlers;
public class GuildAvailableMessageHandler : INotificationHandler<GuildAvailableMessage>
{
    private readonly DiscordContext _db;
    private readonly ILogger<GuildAvailableMessage> _logger;
    public GuildAvailableMessageHandler(DiscordContext db, ILogger<GuildAvailableMessage> logger)
    {
        _db = db;
        _logger = logger;
    }


    public async Task Handle(GuildAvailableMessage message, CancellationToken cancellationToken)
    {
        var guilds = await _db
                .Guilds
                .Select(x => x.GuildId)
                .ToListAsync();

        var users = await _db
            .Users
            .Select(x => x.DiscordId)
            .ToListAsync();


        var members = await message.Guild.GetAllMembersAsync();

        List<UserModel> temp = new();
        foreach(var member in members)
        {
            if(!users.Contains(member.Id))
            {
                temp.Add(new UserModel(member.Id));
            }
        }

        if(temp.Count > 0)
        {
            _db.Users.AddRange(temp);
            _db.SaveChanges();
        }

        if(!guilds.Contains(message.Guild.Id))
        {
            var g = await _db.Guilds.AddAsync(GuildModel.CreateDefaultGuild(message.Guild.Id));
            _logger.LogInformation("Added {g} to database.", g.Entity.GuildId);
            _db.SaveChanges();
        }
    }
}

