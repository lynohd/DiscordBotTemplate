using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.ConsoleUI.Services;
public class StatusMessageProvider : IStatusMessageProvider
{
    private readonly IConfiguration _config;
    private readonly ILogger<StatusMessageProvider> _logger;

    public StatusMessageProvider(IConfiguration config, ILogger<StatusMessageProvider> logger)
    {
        _config = config;
        _logger = logger;
    }


    public string GetStatusMessage()
    {
        var section = _config.GetSection("Discord:StatusMessages");
        var list = section.GetChildren().Select(x => x.Value).ToList();

        if(list is null || list.Count <= 0)
        {
            _logger.LogInformation("No status messages was found in appconfig.json");
            return string.Empty;
        }
        var rng = Random.Shared.Next(0, list.Count);
        _logger.LogInformation("Found {Count} Status messages in  AppConfig.json::Discord:StatusMessages and {Current} has been selected to be shown.", list.Count, list[rng]);

        return list[rng]!;
    }


}
