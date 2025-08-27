using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogProject.DataAccessLayer.Migrations
{
    public partial class mig8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Writers_Writers_WriterId1",
                table: "Writers");

            migrationBuilder.DropIndex(
                name: "IX_Writers_WriterId1",
                table: "Writers");

            migrationBuilder.DropColumn(
                name: "WriterId1",
                table: "Writers");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Receiver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "WriterId1",
                table: "Writers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Writers_WriterId1",
                table: "Writers",
                column: "WriterId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Writers_Writers_WriterId1",
                table: "Writers",
                column: "WriterId1",
                principalTable: "Writers",
                principalColumn: "WriterId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
