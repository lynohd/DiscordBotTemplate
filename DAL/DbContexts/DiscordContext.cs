using DiscordBot.DataAccess.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.DataAccess.DbContexts;
public class DiscordContext : DbContext
{
    public DbSet<GuildModel> Guilds { get; set; }
    public DbSet<UserModel> Users { get; set; }

    public DiscordContext(DbContextOptions options) : base(options)
    {
    }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Discord;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    //}
}
