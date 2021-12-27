using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.DataLayer.Migrations
{
    public partial class initroletabledata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "IsDelete", "LastUpdateDate", "Name", "Title" },
                values: new object[] { 1L, new DateTime(2021, 12, 28, 0, 8, 30, 432, DateTimeKind.Local).AddTicks(3148), false, new DateTime(2021, 12, 28, 0, 8, 30, 436, DateTimeKind.Local).AddTicks(694), "Admin", "ادمین سایت" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "IsDelete", "LastUpdateDate", "Name", "Title" },
                values: new object[] { 2L, new DateTime(2021, 12, 28, 0, 8, 30, 436, DateTimeKind.Local).AddTicks(3635), false, new DateTime(2021, 12, 28, 0, 8, 30, 436, DateTimeKind.Local).AddTicks(3671), "SuperAdmin", "ادمین سایت" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
