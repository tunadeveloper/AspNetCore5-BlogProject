using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogProject.DataAccessLayer.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WriterId1",
                table: "Writers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WriterId",
                table: "Blogs",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Writers_WriterId1",
                table: "Writers",
                column: "WriterId1");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_WriterId",
                table: "Blogs",
                column: "WriterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Writers_WriterId",
                table: "Blogs",
                column: "WriterId",
                principalTable: "Writers",
                principalColumn: "WriterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Writers_Writers_WriterId1",
                table: "Writers",
                column: "WriterId1",
                principalTable: "Writers",
                principalColumn: "WriterId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Writers_WriterId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Writers_Writers_WriterId1",
                table: "Writers");

            migrationBuilder.DropIndex(
                name: "IX_Writers_WriterId1",
                table: "Writers");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_WriterId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "WriterId1",
                table: "Writers");

            migrationBuilder.DropColumn(
                name: "WriterId",
                table: "Blogs");
        }
    }
}
