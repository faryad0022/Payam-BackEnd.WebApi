using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.DataLayer.Migrations
{
    public partial class initroletabledata1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LastUpdateDate" },
                values: new object[] { new DateTime(2021, 12, 28, 0, 40, 49, 154, DateTimeKind.Local).AddTicks(2142), new DateTime(2021, 12, 28, 0, 40, 49, 160, DateTimeKind.Local).AddTicks(1741) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreateDate", "LastUpdateDate", "Title" },
                values: new object[] { new DateTime(2021, 12, 28, 0, 40, 49, 160, DateTimeKind.Local).AddTicks(6870), new DateTime(2021, 12, 28, 0, 40, 49, 160, DateTimeKind.Local).AddTicks(6934), "سوپر ادمین" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "IsDelete", "LastUpdateDate", "Name", "Title" },
                values: new object[,]
                {
                    { 3L, new DateTime(2021, 12, 28, 0, 40, 49, 160, DateTimeKind.Local).AddTicks(7016), false, new DateTime(2021, 12, 28, 0, 40, 49, 160, DateTimeKind.Local).AddTicks(7025), "Secreter", "منشی" },
                    { 4L, new DateTime(2021, 12, 28, 0, 40, 49, 160, DateTimeKind.Local).AddTicks(7032), false, new DateTime(2021, 12, 28, 0, 40, 49, 160, DateTimeKind.Local).AddTicks(7039), "Blogger", "بلاگر" },
                    { 5L, new DateTime(2021, 12, 28, 0, 40, 49, 160, DateTimeKind.Local).AddTicks(7047), false, new DateTime(2021, 12, 28, 0, 40, 49, 160, DateTimeKind.Local).AddTicks(7054), "Advertiser", "تبلیغاتی" },
                    { 6L, new DateTime(2021, 12, 28, 0, 40, 49, 160, DateTimeKind.Local).AddTicks(7062), false, new DateTime(2021, 12, 28, 0, 40, 49, 160, DateTimeKind.Local).AddTicks(7069), "User", "کاربر معمولی" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "CreateDate", "Email", "EmailActiveCode", "FirstName", "IsActivated", "IsDelete", "LastName", "LastUpdateDate", "Password" },
                values: new object[] { 1L, "کوی فراز - خیابان مینا - پلاک 5 - واحد 17", new DateTime(2021, 12, 28, 0, 40, 49, 163, DateTimeKind.Local).AddTicks(8974), "mahancomputer49@gmail.com", null, "فریاد", true, false, "ابوالحسنی", new DateTime(2021, 12, 28, 0, 40, 49, 163, DateTimeKind.Local).AddTicks(9071), "21-C2-10-1A-38-F7-32-8A-67-E0-B6-E1-09-23-27-89" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "LastUpdateDate" },
                values: new object[] { new DateTime(2021, 12, 28, 0, 8, 30, 432, DateTimeKind.Local).AddTicks(3148), new DateTime(2021, 12, 28, 0, 8, 30, 436, DateTimeKind.Local).AddTicks(694) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreateDate", "LastUpdateDate", "Title" },
                values: new object[] { new DateTime(2021, 12, 28, 0, 8, 30, 436, DateTimeKind.Local).AddTicks(3635), new DateTime(2021, 12, 28, 0, 8, 30, 436, DateTimeKind.Local).AddTicks(3671), "ادمین سایت" });
        }
    }
}
