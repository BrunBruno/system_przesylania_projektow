using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace system_przesylania_projektow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UndoChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solutions_Tasks_TaskId",
                table: "Solutions");

            migrationBuilder.DropIndex(
                name: "IX_Solutions_TaskId",
                table: "Solutions");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "Solutions");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "TaskId",
                table: "Solutions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_TaskId",
                table: "Solutions",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Solutions_Tasks_TaskId",
                table: "Solutions",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
