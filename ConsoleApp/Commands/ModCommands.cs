using DiscordBot.DataAccess.Models;

using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

using Microsoft.Extensions.Logging;

using System.Net.Http.Json;

namespace DiscordBot.ConsoleUI.Commands;
public class ModCommands : BaseCommandModule
{
    private readonly ILogger<ModCommands> _logger;
    private readonly HttpClient _client;

    public ModCommands(ILogger<ModCommands> logger, IHttpClientFactory factory)
    {
        _logger = logger;
        _client = factory.CreateClient("Api");
    }

    [Command("prefix"), Aliases("setprefix")]
    public async Task SetPrefix(CommandContext ctx, string prefix)
    {

        var result = await _client.PostAsync($"discord/guild/{ctx.Guild.Id}/prefix?value={prefix}", null);


        if(result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            await ctx.RespondAsync("Prefix has been set to: " + prefix);
            _logger.LogInformation("{Guild} has successfully changed prefix to {prefix}", ctx.Guild.Id, prefix);
            return;
        }

        _logger.LogInformation("Something went wrong");
        await ctx.RespondAsync("Something went wrong");
    }

    [Command("setchannel")]
    public async Task SetBotChannel(CommandContext ctx, DiscordChannel channel)
    {

        var result = await _client.PostAsync($"discord/guild/{ctx.Guild.Id}/channel?channelId={channel.Id}", null);
        var guildModel = await result.Content.ReadFromJsonAsync<GuildModel>();


        if(result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var mention = ctx.Guild.GetChannel(guildModel.BotChannel).Mention;
            await ctx.RespondAsync($"{mention} has been set as your default bot channel.");
            return;
        }

        await ctx.RespondAsync("something went wrong");
        _logger.LogInformation("something went wrong");
    }

    [Command("getchannel")]
    public async Task GetBotChannel(CommandContext ctx)
    {

        var result = await _client.GetAsync($"discord/guild/{ctx.Guild.Id}/");

        var guildModel = await result.Content.ReadFromJsonAsync<GuildModel>();


        if(result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            //ctx.Channel.Mention

            var mention = ctx.Guild.GetChannel(guildModel.BotChannel).Mention;

            //var channel = ctx.Guild.GetChannel(result)
            await ctx.RespondAsync($"{mention} is your bot channel.");
            return;
        }
        Console.WriteLine(result.StatusCode);

    }
}
