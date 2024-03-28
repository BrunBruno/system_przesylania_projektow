using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace system_przesylania_projektow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solutions_Tasks_ProjectTaskId",
                table: "Solutions");

            migrationBuilder.DropIndex(
                name: "IX_Solutions_ProjectTaskId",
                table: "Solutions");

            migrationBuilder.DropColumn(
                name: "ProjectTaskId",
                table: "Solutions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectTaskId",
                table: "Solutions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_ProjectTaskId",
                table: "Solutions",
                column: "ProjectTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Solutions_Tasks_ProjectTaskId",
                table: "Solutions",
                column: "ProjectTaskId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }
    }
}
