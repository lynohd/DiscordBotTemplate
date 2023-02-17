using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace DiscordBot.DataAccess.Models;
[PrimaryKey(nameof(DiscordId))]
public class UserModel
{

    [Required, Key]
    public ulong DiscordId { get; set; }

    [Required]
    public int Points { get; set; } = 0;

    public UserModel(ulong discordId)
    {
        DiscordId = discordId;
    }



    public override string ToString()
    {
        return $"{DiscordId}";
    }

    public static implicit operator ulong(UserModel user) => user.DiscordId;
}
