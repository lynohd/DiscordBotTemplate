using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shared.Migrations
{
    /// <inheritdoc />
    public partial class AddChannel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<decimal>(
            //    name: "DiscordId",
            //    table: "Users",
            //    type: "decimal(20,0)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "decimal(20,0)")
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "GuildId",
            //    table: "Guilds",
            //    type: "decimal(20,0)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "decimal(20,0)")
            //    .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<decimal>(
                name: "BotChannel",
                table: "Guilds",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BotChannel",
                table: "Guilds");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "DiscordId",
            //    table: "Users",
            //    type: "decimal(20,0)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "decimal(20,0)")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "GuildId",
            //    table: "Guilds",
            //    type: "decimal(20,0)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "decimal(20,0)")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
