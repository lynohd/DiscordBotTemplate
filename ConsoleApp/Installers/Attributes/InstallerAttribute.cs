namespace DiscordBot.ConsoleUI.Installers.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public sealed class InstallerAttribute : Attribute
{
    public InstallerAttribute(bool enabled)
    {
        Enabled = enabled;
    }

    public bool Enabled { get; }
}