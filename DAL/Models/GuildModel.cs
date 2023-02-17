using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace DiscordBot.DataAccess.Models;


[PrimaryKey(nameof(GuildId))]
public class GuildModel
{
    [Required, Key]
    public ulong GuildId { get; set; }

    public ulong BotChannel { get; set; } = default;
    public string Prefix { get; set; } = ";;";

    public GuildModel(ulong guildId)
    {
        GuildId = guildId;
    }


    public static GuildModel CreateDefaultGuild(ulong id)
    {
        return new GuildModel(id) { Prefix = ";;" };
    }

    public static implicit operator ulong(GuildModel model)
    {
        return model.GuildId;
    }
}
