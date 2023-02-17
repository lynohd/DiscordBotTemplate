using DiscordBot.DataAccess.DbContexts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiscordBot.WebAPI.Controllers.Discord;
[ApiController]
[Route("api/discord/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly DiscordContext _db;

    public UserController(ILogger<UserController> logger, DiscordContext db)
    {
        _logger = logger;
        _db = db;
    }



    [HttpGet, Route("{id}")]
    public async Task<IActionResult> GetUser(ulong id)
    {
        var data = _db.Users.Where(x => x.DiscordId == id).FirstOrDefault();
        return Ok(data);
    }


    [HttpGet("/all")]
    public async Task<IActionResult> GetUsers()
    {
        var data = await _db.Users.ToListAsync();
        return Ok(data);
    }

}


