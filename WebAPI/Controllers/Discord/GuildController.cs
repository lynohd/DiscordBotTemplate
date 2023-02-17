using DiscordBot.DataAccess.DbContexts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiscordBot.WebAPI.Controllers.Discord;
[ApiController]
[Route("api/discord/[controller]")]
public class GuildController : ControllerBase
{
    private readonly ILogger<GuildController> _logger;
    private readonly DiscordContext _db;

    public GuildController(ILogger<GuildController> logger, DiscordContext db)
    {
        _logger = logger;
        _db = db;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGuild(ulong id)
    {
        var guild = _db.Guilds.Where(x => x.GuildId == id).FirstOrDefault();

        return Ok(guild);
    }


    [HttpGet("{id}/prefix")]
    public async Task<IActionResult> GetPrefix(ulong id)
    {
        var guild = _db.Guilds.Where(x => x.GuildId == id).FirstOrDefault();

        return Ok(new { guild.Prefix });
    }

    [HttpPost("{id}/prefix")]

    public async Task<IActionResult> SetPrefix(ulong id, [FromQuery] string value)
    {
        try
        {
            var guild = _db.Guilds.Where(x => x.GuildId == id).FirstOrDefault();

            _db.Entry(guild).State = EntityState.Modified;
            guild.Prefix = value;
            _db.SaveChanges();

            _logger.LogInformation("{Guild} has changed prefix to {prefix}", guild.GuildId, guild.Prefix);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetGuilds()
    {
        var data = await _db.Guilds.ToListAsync();
        return Ok(data);
    }


    [HttpPost("{id}/channel/")]
    public async Task<IActionResult> SetBotChannel(ulong id, [FromQuery] ulong channelId)
    {
        var guild = _db.Guilds.Where(x => x.GuildId == id).FirstOrDefault();

        _db.Entry(guild).State = EntityState.Modified;
        guild.BotChannel = channelId;
        _db.SaveChanges();

        _logger.LogInformation("{Guild} has changed the channel to {channel}", guild.GuildId, channelId);
        return Ok(guild);
    }


}


