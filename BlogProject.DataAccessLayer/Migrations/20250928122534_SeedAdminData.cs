using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogProject.DataAccessLayer.Migrations
{
    public partial class SeedAdminData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 1, "414cc39b-cc0d-489f-95b1-4552f34a4568", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "6ad1d784-d08c-435f-b211-80fe799062d8", "admin@blog.com", true, false, null, "ADMIN@BLOG.COM", "ADMIN@BLOG.COM", "AQAAAAEAACcQAAAAEOGiENha0yBbpy025QfQA9Ml7woRba3yW4jhFpYRxe9X3xWUt86WvXJFaMgfkqIYeA==", null, false, "6261c491-f9d4-4434-8127-7b51ad37471c", false, "admin@blog.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
