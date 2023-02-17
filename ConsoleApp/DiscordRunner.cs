using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DiscordBot.DataAccess.DbContexts;
using DiscordBot.Shared.Services;

namespace DiscordBot.ConsoleUI;
public class DiscordRunner : BackgroundService
{

    IDiscordService _service;
    ILogger<DiscordRunner> _logger;
    IPublisher _publisher;
    DiscordContext _db;

    Dictionary<string, Action> _cmds = new Dictionary<string, Action>();


    public DiscordRunner(IDiscordService service,
        ILogger<DiscordRunner> logger,
        IPublisher publisher,
        DiscordContext db)
    {
        _service = service;
        _logger = logger;
        _publisher = publisher;
        _db = db;
        RegisterCommands();
    }

    public void RegisterCommands()
    {
        _cmds.Add("list", () =>
        {
            Console.WriteLine("Listing available commands: ");
            foreach(var cmd in _cmds)
            {
                Console.WriteLine(cmd.Key);
            }
        });

        _cmds.Add("start", async () => await _service.StartAsync());

        _cmds.Add("stop", async () => await _service.StopAsync());

        _cmds.Add("say", () =>
        {
            Console.Write("Enter Message: ");
            var msg = Console.ReadLine();
        });

        _cmds.Add("clear", Console.Clear);

        _cmds.Add("guilds", () =>
        {
            foreach(var g in _service.DiscordClient.Guilds)
            {
                Console.WriteLine(g);
            }
        });

        _cmds.Add("channels", () =>
        {
            foreach(var g in _service.DiscordClient.Guilds)
            {
                Console.WriteLine(g.Value.Name);
                foreach(var c in g.Value.Channels)
                {
                    Console.WriteLine("\t[" + c.Value.Name + "]");
                }
            }
        });

        _cmds.Add("users", () =>
        {
            Console.WriteLine("Amount of users: " + _db.Users.ToList().Count);
            foreach(var user in _db.Users)
            {

                var username = _service.DiscordClient.GetUserAsync(user).Result.Username;
                Console.WriteLine(username);
            }
        });

        _cmds.Add("addpoints", async () =>
        {
            Console.Write("Enter UserId: ");
            ulong enterId = ulong.Parse(Console.ReadLine());

            Console.Write("How many points do u want to add? ");
            int amount = int.Parse(Console.ReadLine());

            var user = await _db.Users.Where(x => x.DiscordId == enterId).FirstOrDefaultAsync();
            user.Points += amount;

            Console.WriteLine($"you added {amount} to {user} they now have {user.Points}");
            await _db.SaveChangesAsync();
        });

        _cmds.Add("getuser", async () =>
        {
            Console.Write("Enter UserId: ");
            ulong enterId = ulong.Parse(Console.ReadLine());
            var user = await _db.Users.Where(x => x.DiscordId == enterId).FirstOrDefaultAsync();
            Console.WriteLine(user + " points: " + user.Points);
        });
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _service.StartAsync();

        while(true)
        {
            Console.Write(">");
            var input = Console.ReadLine()!;
            if(_cmds.ContainsKey(input))
            {
                _cmds[input]();
                continue;
            }
            Console.WriteLine("Not a valid command");
        }
    }
}
