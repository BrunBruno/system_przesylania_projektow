using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace system_przesylania_projektow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixSolution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Solutions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Solutions",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
