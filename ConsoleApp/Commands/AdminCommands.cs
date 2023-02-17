using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.ConsoleUI.Commands;
public class AdminCommands : BaseCommandModule
{
    [Command("test")]
    public async Task Test(CommandContext ctx, string args)
    {
        Console.WriteLine("TEST");

    }
}
